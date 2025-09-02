using Patient.Domain.Entities;

namespace Patient.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient.Domain.Entities.Patient> GetByIdAsync(int id);
        Task<IEnumerable<Patient.Domain.Entities.Patient>> GetAllAsync();
        Task<Patient.Domain.Entities.Patient> AddAsync(Patient.Domain.Entities.Patient patient);
        Task UpdateAsync(Patient.Domain.Entities.Patient patient);
        Task DeleteAsync(int id);
    }
}