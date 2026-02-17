using HIMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Staff> Staffs { get; set; }  =null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
    }
}
