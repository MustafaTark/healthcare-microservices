using Microsoft.AspNetCore.Mvc;
using SharedKernal.Mediator;

namespace Clinics.Features.UpdateClinic
{
    public static class UpdateClinicEndpoint
    {
        public static void MapUpdateClinicEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/api/clinics/{id}", async (
                Guid id,
                [FromBody] UpdateClinicCommand command,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                command = command with { Id = id };
                var result = await mediator.SendAsync(command, cancellationToken);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateClinic")
            .WithTags("Clinics");
        }
    }
}