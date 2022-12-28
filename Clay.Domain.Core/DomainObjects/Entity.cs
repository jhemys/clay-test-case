namespace Clay.Domain.Core.DomainObjects
{
    public abstract class Entity : IEquatable<Entity>
    {
        public int Id { get; protected set; }

        public bool Equals(Entity? other)
        {
            return Id == other.Id;
        }
    }
}
