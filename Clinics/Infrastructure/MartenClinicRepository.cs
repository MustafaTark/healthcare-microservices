using Clinics.Domain;
using Marten;

namespace Clinics.Infrastructure
{
    public class MartenClinicRepository : IClinicRepository
    {
        private readonly IDocumentSession _session;

        public MartenClinicRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Guid> CreateAsync(Clinic clinic, CancellationToken cancellationToken)
        {
            _session.Store(clinic);
            await _session.SaveChangesAsync(cancellationToken);
            return clinic.Id;
        }

        public async Task<Clinic?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _session.LoadAsync<Clinic>(id, cancellationToken);
        }
    }
}