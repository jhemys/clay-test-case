using Clay.Domain.DomainObjects;

namespace Clay.Domain
{
    public class State : ValueObject
    {
        public string Name { get; set; }
        public int MyProperty { get; set; }
    }
}