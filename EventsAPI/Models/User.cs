using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public class User 
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public required string Name { get; set; }
        public string Surname { get; set; }
        public List<User> Friends { get; set; } = new ();
        public List<Participation> EventParticipations { get; set; } = new();
        public List<InvitationSending> InvitationSendings { get; set; } = new();
        public List<InvitationReceiving> InvitationReceivings { get; set; } = new();

    }
}
