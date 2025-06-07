using Clinics.Domain;
using SharedKernal.Mediator.Interfaces;

namespace Clinics.Features.GetAllClinics
{
    public record GetAllClinicsQuery : IQuery<IReadOnlyList<Clinic>>;
}