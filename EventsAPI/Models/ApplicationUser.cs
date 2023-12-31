namespace EventsAPI.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public User EventUser { get; set; }
    }

    
}
