using Clay.Domain.DomainObjects;

namespace Clay.Domain.Aggregates.Door
{
    public class Role : Entity, IEquatable<Role>
    {
        public string Name { get; set; }
        public virtual IReadOnlyCollection<Door> Doors { get; set; }
        public Role(string name)
        {
            Name = name;
        }

        public bool Equals(Role? other) => Name == other?.Name;
    }
}