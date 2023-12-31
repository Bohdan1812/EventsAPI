namespace EventsAPI.Models.DTOs
{
    public class AddInvitationDto
    {
        public Guid EventId { get; set; }
        public InvitationStatus Status { get; set; } = 0;
        public InvitationSending Sending { get; set; }
        public InvitationReceiving Receiving { get; set; }
    }
}
