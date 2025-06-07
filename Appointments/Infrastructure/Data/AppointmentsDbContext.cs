using Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Data;

public class AppointmentsDbContext : DbContext
{
    public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ClinicId).IsRequired();
            entity.Property(e => e.DateFrom).IsRequired();
            entity.Property(e => e.DateTo).IsRequired();
            entity.Property(e => e.TimeFrom).IsRequired();
            entity.Property(e => e.TimeTo).IsRequired();
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AppointmentId).IsRequired();
            entity.Property(e => e.ClinicId).IsRequired();
            entity.Property(e => e.ClinicName).IsRequired();
            entity.Property(e => e.PatientId).IsRequired();
            entity.Property(e => e.PatientName).IsRequired();
            entity.Property(e => e.PatientIssue).IsRequired();

            entity.HasOne(e => e.Appointment)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
} 