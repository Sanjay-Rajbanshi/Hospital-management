using System;
using HIMS.Model;

namespace HIMS.DTOs
{
    public class AppointmentDto
    {
       
        public Guid Id { get; set; }

        
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }

    public class UpdateAppointmentDto
    {

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
       

    }
}
