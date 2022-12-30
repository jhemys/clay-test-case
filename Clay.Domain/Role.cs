using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public Role(string name)
        {
            Name = name;
        }
    }
}