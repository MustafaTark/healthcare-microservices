using Clinics.Features.CreateClinic;
using Clinics.Features.GetAllClinics;
using Clinics.Features.GetClinicsByName;
using Clinics.Features.UpdateClinic;
using Clinics.Features.DeleteClinic;
using Clinics.Infrastructure;
using FluentValidation;
using SharedKernal.Mediator.DependencyInjection;
using Marten;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;
using Polly;

namespace Clinics
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Marten
            MartenConfig.ConfigureMarten(builder.Services, builder.Configuration);

            // Add repositories     
            builder.Services.AddScoped<IClinicRepository, MartenClinicRepository>();

            // Add mediator and validators
            builder.Services.AddMediator();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateClinicValidator>();

            // Add health checks
            builder.Services.AddHealthChecks()
                .AddNpgSql(
                    builder.Configuration.GetConnectionString("DefaultConnection")!,
                    name: "postgres",
                    tags: new[] { "ready" });

            var app = builder.Build();

            // Add health check endpoint
            app.MapHealthChecks("/health");

            // Ensure database is created with retry policy
            await EnsureDatabaseAsync(app);

            // Map all endpoints
            app.MapCreateClinicEndpoint();
            app.MapGetAllClinicsEndpoint();
            app.MapGetClinicsByNameEndpoint();
            app.MapUpdateClinicEndpoint();
            app.MapDeleteClinicEndpoint();

            app.Run();
        }

        private static async Task EnsureDatabaseAsync(WebApplication app)
        {
            var retryPolicy = Policy
                .Handle<NpgsqlException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(5, retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        app.Logger.LogWarning(
                            exception,
                            "Failed to connect to database. Retry attempt {RetryCount} after {TimeBetweenRetries}s",
                            retryCount,
                            timeSpan.TotalSeconds);
                    }
                );

            try
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    using var scope = app.Services.CreateScope();
                    var store = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
                    
                    // Test connection before applying changes
                    using var conn = new NpgsqlConnection(
                        app.Configuration.GetConnectionString("DefaultConnection"));
                    await conn.OpenAsync();
                    
                    // Apply database changes
                    await store.Storage.ApplyAllConfiguredChangesToDatabaseAsync();
                });
            }
            catch (Exception ex)
            {
                app.Logger.LogError(
                    ex,
                    "Failed to initialize database after multiple retries. Application startup will continue, but database operations may fail");
                
                // Optionally, you might want to throw here to prevent application startup
                // throw;
            }
        }
    }
}
