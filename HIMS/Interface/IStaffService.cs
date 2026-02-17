using HIMS.Model;


namespace HIMS.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllStaffsAsync();
        Task<Staff> GetStaffByIdAsync(Guid id);
        Task<Staff> AddStaffAsync(Staff staff);
        Task<Staff> UpdateStaffAsync(Staff staff);
        Task<bool> DeleteStaffAsync(Guid id);
    }
}
