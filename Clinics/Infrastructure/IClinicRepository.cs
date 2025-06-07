using Clinics.Domain;

namespace Clinics.Infrastructure
{
    public interface IClinicRepository
    {
        Task<Guid> CreateAsync(Clinic clinic, CancellationToken cancellationToken);
        Task<Clinic?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}