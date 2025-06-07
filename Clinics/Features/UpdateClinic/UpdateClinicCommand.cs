using SharedKernal.Mediator.Commands;

namespace Clinics.Features.UpdateClinic
{
    public record UpdateClinicCommand : Command<bool>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }
}