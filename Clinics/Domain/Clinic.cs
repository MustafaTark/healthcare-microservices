using System.Text.Json.Serialization;

namespace Clinics.Domain
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> ExtendedData { get; set; } = new();
    }
}