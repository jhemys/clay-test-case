using Clay.Domain.DomainObjects;
using Clay.Domain.Validations;

namespace Clay.Domain.Aggregates.Employee
{
    public class Employee : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Role { get; protected set; }
        public string? TagIdentification { get; set; }
        private Employee() : base() { }

        protected Employee(string name, string role, string? tagIdentification) : this()
        {
            Name = name;
            Role = role;
            TagIdentification = tagIdentification;
        }

        public static Employee Create(string name, string role, string? tagIdentification = null)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");
            Throw.IfArgumentIsNullOrWhitespace(role, "The parameter Role is required.");

            return new Employee(name, role, tagIdentification);
        }

        public void SetName(string name)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");

            Name = name;
        }

        public void SetRole(string role)
        {
            Throw.IfArgumentIsNullOrWhitespace(role, "The parameter Role is required.");

            Role = role;
        }
    }
}
