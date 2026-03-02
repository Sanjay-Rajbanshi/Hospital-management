using HIMS.Data;
using HIMS.Interfaces;
using HIMS.Model;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        // Get all appointments
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                                 .Include(a => a.Patient)   // Load patient
                                 .Include(a => a.Doctor)     // Load staff
                                 .ToListAsync();
        }

        // Get by id
        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
#pragma warning disable CS8603
            return await _context.Appointments
                                 .Include(a => a.Patient)
                                 .Include(a => a.Doctor)
                                 .FirstOrDefaultAsync(a => a.Id == id)!;

        }

        // Add new appointment
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            var staff = await _context.Staffs.FindAsync(appointment.DoctorId);
            if (staff == null) throw new Exception("Staff not found");

            var patient = await _context.Patients.FindAsync(appointment.PatientId);
            if (patient == null) throw new Exception("Patient not found");

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();


#pragma warning disable CS8603
            return await _context.Appointments
                                 .Include(a => a.Patient)
                                 .Include(a => a.Doctor)
                                 .FirstOrDefaultAsync(a => a.Id == appointment.Id)!;
#pragma warning restore CS8603 
        }

        // Update appointment
        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

#pragma warning disable CS8603
            return await _context.Appointments
                                 .Include(a => a.Patient)
                                 .Include(a => a.Doctor)
                                 .FirstOrDefaultAsync(a => a.Id == appointment.Id)!;

        }

        // Cancel appointment
        public async Task<bool> CancelAppointmentAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return false;

            appointment.AppointmentStatus = AppointmentStatus.Cancelled;

            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}
