using Microsoft.EntityFrameworkCore;
using Patient.Application.Interfaces;
using Patient.Domain.Entities;
using Patient.Infrastructure.Data;

namespace Patient.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public async Task<Patient.Domain.Entities.Patient> GetByIdAsync(int id)
            => await _context.Patients.FindAsync(id) ?? throw new KeyNotFoundException();

        public async Task<IEnumerable<Patient.Domain.Entities.Patient>> GetAllAsync()
            => await _context.Patients.ToListAsync();

        public async Task<Patient.Domain.Entities.Patient> AddAsync(Patient.Domain.Entities.Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task UpdateAsync(Patient.Domain.Entities.Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await GetByIdAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}