using Clay.Domain.Core.DomainObjects;
using Clay.Domain.Validations;

namespace Clay.Domain
{
    public class Employee : Entity
    {
        public string Name { get; protected set; }
        public Role Role { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public Employee() : base() { }

        protected Employee(string name, string role, string password, string email) : base()
        {
            Name = name;
            Role = new Role(role);
            Password = password;
            Email = email;
        }

        public static Employee Create(string name, string role, string password, string email)
        {
            Throw.IfArgumentIsNullOrEmpty(name, "The parameter Name is required.");
            Throw.IfArgumentIsNullOrEmpty(role, "The parameter Role is required.");
            Throw.IfArgumentIsNullOrEmpty(email, "The parameter Email is required.");
            Throw.IfArgumentIsNullOrEmpty(password, "The parameter Password is required.");

            return new Employee(name, role, password, email);
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            Throw.IfAssertArgumentsAreEqual(Password, currentPassword, "Current Password is invalid.");
            Throw.IfAssertArgumentsAreNotEquals(newPassword, currentPassword, "New Password must be different than Current Password.");

            Password = newPassword;
        }

        public void SetName(string name)
        {
            Throw.IfArgumentIsNullOrEmpty(name, "The parameter Name is required.");

            Name = name;
        }

        public void SetEmail(string email)
        {
            Throw.IfArgumentIsNullOrEmpty(email, "The parameter Email is required.");

            Email = email;
        }

        public void SetRole(string role)
        {
            Throw.IfArgumentIsNullOrEmpty(role, "The parameter Role is required.");

            Role = new Role(role);
        }
    }
}
