using Microsoft.AspNetCore.Mvc;
using SharedKernal.Mediator;

namespace Clinics.Features.DeleteClinic
{
    public static class DeleteClinicEndpoint
    {
        public static void MapDeleteClinicEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/clinics/{id}", async (
                Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.SendAsync(new DeleteClinicCommand(id), cancellationToken);
                return result.Value ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteClinic")
            .WithTags("Clinics");
        }
    }
}