using MediatR;

namespace Appointments.Features.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand : IRequest<int>
{
    public int ClinicId { get; init; }
    public DateTime DateFrom { get; init; }
    public DateTime DateTo { get; init; }
    public TimeSpan TimeFrom { get; init; }
    public TimeSpan TimeTo { get; init; }
} 