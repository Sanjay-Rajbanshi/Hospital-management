using System;

namespace HIMS.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }

        // Link to Patient
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        // Link to Staff (Doctor/Nurse/Admin)
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = "Booked"; // Booked, Cancelled

        public string Notes { get; set; } // Optional
    }
}
