using Clay.Domain.Core.DomainObjects;

namespace Clay.Domain
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Employee() { }  

        protected Employee(string name, string role, string password, string email)
        {
            Name = name;
            Role = role;
            Password = password;
            Email = email;
        }

        public static Employee Create(string name, string role, string password, string email)
        {
            return new Employee(name, role, password, email);
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (Password != currentPassword)
                throw new DomainException("Current Password invalid.");

            if (newPassword == currentPassword)
                throw new DomainException("New Password must be different than Current Password.");

            Password = newPassword;
        }
    }
}
