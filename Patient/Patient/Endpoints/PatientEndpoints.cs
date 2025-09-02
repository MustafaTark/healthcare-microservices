using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Patient.Application.Interfaces;
using Patient.Domain.Entities;

namespace Patient.Endpoints
{
    public static class PatientEndpoints
    {
        public static void MapPatientEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/patients")
                          .WithTags("Patients");

            group.MapGet("/", async (IPatientRepository repo) =>
                await repo.GetAllAsync());

            group.MapGet("/{id}", async (int id, IPatientRepository repo) =>
                await repo.GetByIdAsync(id));

            group.MapPost("/", async (Patient.Domain.Entities.Patient patient, IPatientRepository repo) =>
            {
                await repo.AddAsync(patient);
                return Results.Created($"/patients/{patient.Id}", patient);
            });

            group.MapPut("/{id}", async (int id, Patient.Domain.Entities.Patient patient, IPatientRepository repo) =>
            {
                patient.Id = id;
                await repo.UpdateAsync(patient);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, IPatientRepository repo) =>
            {
                await repo.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}