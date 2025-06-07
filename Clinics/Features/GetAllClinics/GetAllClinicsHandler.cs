using Clinics.Domain;
using Clinics.Infrastructure;
using SharedKernal.Mediator.Interfaces;
using Marten;

namespace Clinics.Features.GetAllClinics
{
    public class GetAllClinicsHandler : IQueryHandler<GetAllClinicsQuery, IReadOnlyList<Clinic>>
    {
        private readonly IDocumentSession _session;

        public GetAllClinicsHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<Clinic>> HandleAsync(
            GetAllClinicsQuery query, 
            CancellationToken cancellationToken)
        {
            return await _session.Query<Clinic>().ToListAsync(cancellationToken);
        }
    }
}