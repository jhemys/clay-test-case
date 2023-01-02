using Clay.Domain.DomainObjects;
using Clay.Domain.DomainObjects.Exceptions;
using Clay.Domain.Validations;

namespace Clay.Domain.Aggregates.Login
{
    public class Login : Entity, IAggregateRoot
    {
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public PermissionType PermissionType { get; set; }
        public virtual Employee.Employee Employee { get; set; }
        private Login() : base() { }

        protected Login(string email, string password, Employee.Employee employee, PermissionType? permissionType) : this()
        {
            Password = password;
            Email = email;
            PermissionType = permissionType ?? PermissionType.Employee;
            Employee = employee;
        }

        public static Login Create(string email, string password, Employee.Employee employee, string? permissionType = null)
        {
            Throw.IfArgumentIsNullOrWhitespace(email, "The parameter Email is required.");
            Throw.IfArgumentIsInvalidEmail(email, "The parameter Email is invalid.");
            Throw.IfArgumentIsNullOrWhitespace(password, "The parameter Password is required.");

            var permissionTypeParsed = PermissionType.None;
            if (permissionType is not null && !Enum.TryParse(permissionType, out permissionTypeParsed))
                throw new DomainException("The parameter PermissionType is invalid.");

            PermissionType? permission = permissionType is null ? null : permissionTypeParsed;

            return new Login(password, email, employee, permission);
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            Throw.IfAssertArgumentsAreNotEqual(Password, currentPassword, "Current Password is invalid.");
            Throw.IfAssertArgumentsAreEqual(newPassword, currentPassword, "New Password must be different than Current Password.");
            Throw.IfArgumentIsNullOrWhitespace(newPassword, "The parameter Password is required.");

            Password = newPassword;
        }

        public void SetEmail(string email)
        {
            Throw.IfArgumentIsNullOrWhitespace(email, "The parameter Email is required.");
            Throw.IfArgumentIsInvalidEmail(email, "The parameter Email is invalid.");

            Email = email;
        }

        public void SetPermissionType(PermissionType permissionType)
        {
            PermissionType = permissionType;
        }
    }
}
