using HIMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Dbhospital
{
    public class AppDb:DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
    
    }
}
