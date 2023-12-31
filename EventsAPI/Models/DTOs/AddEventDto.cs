namespace EventsAPI.Models.DTOs
{
    public class AddEventDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public int? Longitude { get; set; }
        public int? Latitude { get; set; }
        public string? Link { get; set; }
        public EventCreation EventCreation { get; set; }
    }
}
