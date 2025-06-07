using Microsoft.AspNetCore.Mvc;
using SharedKernal.Mediator;

namespace Clinics.Features.GetClinicsByName
{
    public static class GetClinicsByNameEndpoint
    {
        public static void MapGetClinicsByNameEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/clinics/by-name/{name}", async (
                string name,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var clinics = await mediator.QueryAsync(new GetClinicsByNameQuery(name), cancellationToken);
                return Results.Ok(clinics);
            })
            .WithName("GetClinicsByName")
            .WithTags("Clinics");
        }
    }
}