using HIMS.Data;
using HIMS.Interfaces;
using HIMS.Model;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services
{
    public class StaffsService : IStaffService
    {
        private readonly AppDbContext _context;

        public StaffsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffsAsync()
        {
            return await _context.Staffs.ToListAsync();
        }

        public async Task<Staff> GetStaffByIdAsync(Guid id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                throw new KeyNotFoundException($"Staff with id '{id}' was not found.");
            }

            return staff;
        }

        public async Task<Staff> AddStaffAsync(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateStaffAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> DeleteStaffAsync(Guid id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null) return false;

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
