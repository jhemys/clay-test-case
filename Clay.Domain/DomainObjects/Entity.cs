namespace Clay.Domain.DomainObjects
{
    public abstract class Entity : IEquatable<Entity>
    {
        public int Id { get; protected set; }
        public bool IsActive { get; protected set; }

        public Entity() => IsActive = true;

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public bool Equals(Entity? other)
        {
            return Id == other?.Id;
        }
    }
}
