using Clay.Domain.DomainObjects;
using Clay.Domain.Validations;

namespace Clay.Domain.Aggregates.Door
{
    public class Role : Entity, IEquatable<Role>
    {
        public string Name { get; set; }
        public virtual IReadOnlyCollection<Door> Doors { get; set; }

        public Role() : base() { }

        protected Role(string name) : this()
        {
            Name = name;
        }

        public static Role Create(string name)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");

            return new Role(name);
        }

        public bool Equals(Role? other) => Name == other?.Name;
    }
}