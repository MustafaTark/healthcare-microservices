namespace Patient.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string CheckingId { get; set; } = string.Empty;
    }
}