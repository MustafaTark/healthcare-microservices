using SharedKernal.Mediator.Interfaces;

namespace Clinics.Features.CreateClinic
{
    public record CreateClinicCommand(
        string Name,
        string Address,
        string PhoneNumber) : ICommand<Guid>
    {
        public DateTime Timestamp => throw new NotImplementedException();
    }
}