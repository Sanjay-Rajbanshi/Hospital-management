using HIMS.Model;


namespace HIMS.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(Guid id);
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
       
        Task<bool> CancelAppointmentAsync(Guid id);
    }
}
