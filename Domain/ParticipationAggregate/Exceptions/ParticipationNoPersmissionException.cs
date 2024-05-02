namespace Domain.ParticipationAggregate.Exceptions
{
    public class ParticipationNoPersmissionException : Exception
    {
        public ParticipationNoPersmissionException()
            : base("This user have no permission to add participation to this event!")
        { }
    }
}
