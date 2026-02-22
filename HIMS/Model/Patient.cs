namespace HIMS.Model
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address  { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public  string? Gender { get; set; }
        
    }
}
