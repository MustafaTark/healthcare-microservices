using SharedKernal.Mediator.Commands;
using SharedKernal.Results;

namespace Clinics.Features.DeleteClinic
{
    public record DeleteClinicCommand : Command<Result<bool>>
    {
        public Guid Id { get; }

        public DeleteClinicCommand(Guid id)
        {
            Id = id;
        }
    }
}