using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public class Participation
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid? EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; } 
    }
}
