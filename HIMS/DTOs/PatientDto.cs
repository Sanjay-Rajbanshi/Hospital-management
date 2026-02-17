namespace HIMS.DTOs
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Problem { get; set; }
    }
}
