using Clinics.Domain;
using SharedKernal.Mediator.Interfaces;
using Marten;

namespace Clinics.Features.GetClinicsByName
{
    public class GetClinicsByNameHandler : IQueryHandler<GetClinicsByNameQuery, IReadOnlyList<Clinic>>
    {
        private readonly IDocumentSession _session;

        public GetClinicsByNameHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<Clinic>> HandleAsync(
            GetClinicsByNameQuery query, 
            CancellationToken cancellationToken)
        {
            return await _session.Query<Clinic>()
                .Where(c => c.Name.Contains(query.Name))
                .ToListAsync(cancellationToken);
        }
    }
}