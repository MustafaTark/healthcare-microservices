namespace Appointments.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int ClinicId { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PatientIssue { get; set; } = string.Empty;
    
    public virtual Appointment Appointment { get; set; } = null!;
} 