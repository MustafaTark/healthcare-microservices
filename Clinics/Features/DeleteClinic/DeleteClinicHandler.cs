using SharedKernal.Mediator.Interfaces;
using SharedKernal.Results;
using Marten;
using Clinics.Domain;

namespace Clinics.Features.DeleteClinic
{
    public class DeleteClinicHandler : ICommandHandler<DeleteClinicCommand, Result<bool>>
    {
        private readonly IDocumentSession _session;

        public DeleteClinicHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Result<bool>> HandleAsync(
            DeleteClinicCommand command, 
            CancellationToken cancellationToken)
        {
            try
            {
                var clinic = await _session.LoadAsync<Clinic>(command.Id, cancellationToken);
                
                if (clinic == null)
                    return Result.Failure<bool>($"Clinic with ID {command.Id} not found");

                _session.Delete<Clinic>(command.Id);
                await _session.SaveChangesAsync(cancellationToken);

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>($"Failed to delete clinic: {ex.Message}");
            }
        }
    }
}