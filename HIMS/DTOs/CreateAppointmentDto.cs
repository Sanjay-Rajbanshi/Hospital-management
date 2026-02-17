using System;

namespace HIMS.DTOs
{
    public class CreateAppointmentDto
    {
        public Guid PatientId { get; set; } 
        public Guid StaffId { get; set; }   
        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }  
    }
}
