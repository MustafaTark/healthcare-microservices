using Clinics.Domain;
using SharedKernal.Mediator.Interfaces;

namespace Clinics.Features.GetClinicsByName
{
    public record GetClinicsByNameQuery(string Name) : IQuery<IReadOnlyList<Clinic>>;
}