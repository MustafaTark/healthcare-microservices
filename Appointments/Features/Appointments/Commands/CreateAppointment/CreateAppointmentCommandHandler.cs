using Appointments.Domain.Entities;
using Appointments.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
{
    private readonly AppointmentsDbContext _context;

    public CreateAppointmentCommandHandler(AppointmentsDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment
        {
            ClinicId = request.ClinicId,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            TimeFrom = request.TimeFrom,
            TimeTo = request.TimeTo
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
} 