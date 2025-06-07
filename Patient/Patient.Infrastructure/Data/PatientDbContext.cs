using Microsoft.EntityFrameworkCore;
using Patient.Domain.Entities;

namespace Patient.Infrastructure.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Patient.Domain.Entities.Patient> Patients { get; set; }
    }
}