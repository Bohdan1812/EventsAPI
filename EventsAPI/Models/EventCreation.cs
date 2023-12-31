namespace EventsAPI.Models
{
    public class EventCreation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Event> CreatedEvents { get; set; } = new();
    }
}
