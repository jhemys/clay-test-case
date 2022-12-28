using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain
{
    public class State : ValueObject
    {
        public string Name { get; set; }
        public int MyProperty { get; set; }
    }
}