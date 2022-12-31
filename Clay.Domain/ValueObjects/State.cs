using Clay.Domain.DomainObjects;

namespace Clay.Domain.ValueObjects
{
    public class State : ValueObject
    {
        public string Name { get; set; }

        private State() { }

        protected State(string name) : this()
        {
            Name = name;
        }

        public static State Create(string name)
        {
            return new State(name);
        }
    }
}