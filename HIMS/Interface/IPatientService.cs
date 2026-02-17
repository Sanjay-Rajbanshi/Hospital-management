using HIMS.Model;

namespace HIMS.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(Guid id);
    }
}
