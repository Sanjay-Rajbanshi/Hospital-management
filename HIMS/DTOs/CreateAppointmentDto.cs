using HIMS.Model;
using System;

namespace HIMS.DTOs
{
    public class CreateAppointmentDto
    {
        public Guid PatientId { get; set; }
        
        public Guid DoctorId { get; set; }   
        public DateTime AppointmentDateTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
