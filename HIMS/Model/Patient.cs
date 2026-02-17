namespace HIMS.Model
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address  { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
        public string? Problem { get; set; }
        public string? Password { get; set; }
    }
}
