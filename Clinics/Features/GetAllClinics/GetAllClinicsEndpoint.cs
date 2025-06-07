using Microsoft.AspNetCore.Mvc;
using SharedKernal.Mediator;

namespace Clinics.Features.GetAllClinics
{
    public static class GetAllClinicsEndpoint
    {
        public static void MapGetAllClinicsEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/clinics", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var clinics = await mediator.QueryAsync(new GetAllClinicsQuery(), cancellationToken);
                return Results.Ok(clinics);
            })
            .WithName("GetAllClinics")
            .WithTags("Clinics");
        }
    }
}