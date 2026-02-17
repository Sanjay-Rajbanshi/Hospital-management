using HIMS.Model;

namespace HIMS.DTO
{
    public class StaffDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }  // Doctor, Nurse, Admin
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
    }
}
