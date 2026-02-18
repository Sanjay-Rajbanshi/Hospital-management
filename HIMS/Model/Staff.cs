using System;

namespace HIMS.Model
{
    
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int GetAge()
        {
            var today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    // Staff inherits Person
    public class Staff : Person
    {
        public Role Role { get; set; }  // Doctor, Nurse, Admin, etc.
        public string Department { get; set; } // optional
        public string PhoneNumber { get; set; }
    }

    // Enum for roles
    public enum Role
    {
        Doctor,
        Nurse,
        Admin
    }
}
