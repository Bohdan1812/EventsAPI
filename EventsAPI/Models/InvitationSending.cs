using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public class InvitationSending
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Guid? InvitationId {  get; set; } 
        public Invitation Invitation { get; set; }
    }
}
