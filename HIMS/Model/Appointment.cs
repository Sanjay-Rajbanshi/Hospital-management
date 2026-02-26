using System;

namespace HIMS.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }

       
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

     
        public Guid DoctorId { get; set; }
        public Staff Doctor { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public  AppointmentStatus AppointmentStatus { get; set; }

       
    }
}
