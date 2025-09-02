namespace Appointments.Domain.Entities;

public class Appointment
{
    public int Id { get; set; }
    public int ClinicId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public TimeSpan TimeFrom { get; set; }
    public TimeSpan TimeTo { get; set; }
    
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
} 