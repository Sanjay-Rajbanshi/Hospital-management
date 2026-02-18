using System;

namespace HIMS.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }

       
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

     
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }

        public DateTime AppointmentDate { get; set; }

        public  AppointmentStatus AppointmentStatus { get; set; }

        public string Notes { get; set; } 
    }
}
