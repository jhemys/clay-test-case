using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Employee() { }  

        public Employee(string name, Role role, string password, string email)
        {
            Name = name;
            Role = role;
            Password = password;
            Email = email;
        }
    }
}
