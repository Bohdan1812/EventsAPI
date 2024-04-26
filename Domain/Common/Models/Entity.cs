namespace Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
         where TId : notnull
    {
#pragma warning disable CS8618
        protected Entity()
        {

        }
#pragma warning restore CS8618

        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> rigth)
        {
            return Equals(left, rigth);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> rigth)
        {
            return !Equals(left, rigth);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
