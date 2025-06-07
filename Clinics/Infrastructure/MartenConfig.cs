using Marten;
using Marten.Events.Daemon.Resiliency;
using Microsoft.Extensions.Options;
using Weasel.Core;

namespace Clinics.Infrastructure
{
    public class MartenConfig
    {
        public static void ConfigureMarten(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMarten(sp =>
            {
                var options = new StoreOptions();
                
                // Connection string to your PostgreSQL database
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.Connection(connectionString ?? 
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found."));

                // Document mapping for Clinic
                options.Schema.For<Domain.Clinic>()
                    .Identity(x => x.Id)
                    // .Timestamp(x => x.CreatedAt, x => x.UpdatedAt)
                    .Index(x => x.Name);// Add index for common queries
                   // .JsonStorage(compress: true);  // Enable JSON compression

                // Schema settings
                options.DatabaseSchemaName = "clinics";
                options.UseDefaultSerialization(
                    enumStorage: EnumStorage.AsString,  // Store enums as strings
                    casing: Casing.CamelCase           // Use camelCase for JSON properties
                );

                // Development settings
                if (sp.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
                else
                {
                    options.AutoCreateSchemaObjects = AutoCreate.None;
                }

                return options;
            })
            .OptimizeArtifactWorkflow()
            .AddAsyncDaemon(DaemonMode.HotCold)  // Add async daemon for background operations
            .ApplyAllDatabaseChangesOnStartup()   // Apply schema changes
            .InitializeWith();                    // Initialize the store
        }
    }
}