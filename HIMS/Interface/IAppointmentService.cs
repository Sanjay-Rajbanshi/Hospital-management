using HIMS.Model;


namespace HIMS.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(Guid id);
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(Guid id);
    }
}
