namespace Domain.ParticipationAggregate.Exceptions
{
    public class ParticipationExistException : Exception
    {
        public ParticipationExistException()
        : base("This user is already a participant of this event!")
        { }
    }
}
