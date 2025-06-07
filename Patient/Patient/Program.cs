using Microsoft.EntityFrameworkCore;
using Patient.Application.Interfaces;
using Patient.Domain.Entities;
using Patient.Infrastructure.Data;
using Patient.Infrastructure.Repositories;

namespace Patient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddDbContext<PatientDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            var app = builder.Build();

            // CRUD endpoints
            app.MapGet("/patients", async (IPatientRepository repo) =>
                await repo.GetAllAsync());

            app.MapGet("/patients/{id}", async (int id, IPatientRepository repo) =>
                await repo.GetByIdAsync(id));

            app.MapPost("/patients", async (Patient.Domain.Entities.Patient patient, IPatientRepository repo) =>
            {
                await repo.AddAsync(patient);
                return Results.Created($"/patients/{patient.Id}", patient);
            });

            app.MapPut("/patients/{id}", async (int id, Patient.Domain.Entities.Patient patient, IPatientRepository repo) =>
            {
                patient.Id = id;
                await repo.UpdateAsync(patient);
                return Results.NoContent();
            });

            app.MapDelete("/patients/{id}", async (int id, IPatientRepository repo) =>
            {
                await repo.DeleteAsync(id);
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
