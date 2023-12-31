using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public InvitationStatus Status { get; set; } = 0;
        public InvitationSending Sending { get; set; }
        public InvitationReceiving Receiving { get; set; }
    }
}
