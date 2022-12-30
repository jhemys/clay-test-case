using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain.ValueObjects
{
    public class Role : ValueObject, IEquatable<Role>
    {
        public string Name { get; set; }
        public Role(string name)
        {
            Name = name;
        }

        public bool Equals(Role? other) => Name == other?.Name;
    }
}