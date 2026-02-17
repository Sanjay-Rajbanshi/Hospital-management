

namespace HIMS.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public Guid StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffRole { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }

        // Patient info
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }

        // Staff info
        public Guid StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffRole { get; set; } // Doctor, Nurse, Admin

        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
