using Microsoft.AspNetCore.Mvc;
using SharedKernal.Mediator;

namespace Clinics.Features.CreateClinic
{
    public static class CreateClinicEndpoint
    {
        public static void MapCreateClinicEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/clinics", async (
                [FromBody] CreateClinicCommand command,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var clinicId = await mediator.SendAsync(command, cancellationToken);
                return Results.Created($"/api/clinics/{clinicId}", clinicId);
            })
            .WithName("CreateClinic")
            .WithTags("Clinics");
        }
    }
}