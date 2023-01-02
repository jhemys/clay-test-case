using Clay.Domain.DomainObjects.Exceptions;
using LoginAggregate = Clay.Domain.Aggregates.Login.Login;
using EmployeeAggregate = Clay.Domain.Aggregates.Employee.Employee;

namespace Clay.Tests.Domain.Aggregates.Login
{
    public class LoginTests
    {
        [Theory]
        [InlineData("1")]
        [InlineData(null)]
        public void Should_Create_Valid_Entity(string permissionType)
        {
            var employee = EmployeeAggregate.Create("name", "role");
            var loginEntity = LoginAggregate.Create("email@email.com", "password", employee, permissionType);

            loginEntity.Should().NotBeNull();
        }

        [Theory]
        [InlineData("email@email.com", "123456", "5", "The parameter PermissionType is invalid.")]
        [InlineData("email@email.com", "123456", "-1", "The parameter PermissionType is invalid.")]
        [InlineData("email@email.com", "123456", "2", "The parameter PermissionType is invalid.")]
        [InlineData("email@email.com", "123456", "", "The parameter PermissionType is invalid.")]
        [InlineData("email@email.com", "123456", " ", "The parameter PermissionType is invalid.")]
        [InlineData("", "123456", "2", "The parameter Email is required.")]
        [InlineData(" ", "123456", "2", "The parameter Email is required.")]
        [InlineData(null, "123456", "2", "The parameter Email is required.")]
        [InlineData("@email", "123456", "2", "The parameter Email is invalid.")]
        [InlineData("email@email.com", "", "2", "The parameter Password is required.")]
        [InlineData("email@email.com", " ", "2", "The parameter Password is required.")]
        [InlineData("email@email.com", null, "2", "The parameter Password is required.")]
        public void Should_Fail_To_Create_Valid_Entity(string email, string password, string permissionType, string errorMessage)
        {
            var employee = EmployeeAggregate.Create("name", "role");
            var action = () => LoginAggregate.Create(email, password, employee, permissionType);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Theory]
        [InlineData("123456", "123456", "New Password must be different than Current Password.")]
        [InlineData("654321", "", "Current Password is invalid.")]
        [InlineData("123456", "", "The parameter Password is required.")]
        public void ChangePassword_Should_Fail(string currentPassword, string newPassword, string errorMessage)
        {
            var employee = EmployeeAggregate.Create("name", "role");
            var loginEntity = LoginAggregate.Create("email@email.com", "123456", employee);

            var action = () => loginEntity.ChangePassword(currentPassword, newPassword);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Fact]
        public void ChangePassword_Should_Change_Password()
        {
            var employee = EmployeeAggregate.Create("name", "role");
            var loginEntity = LoginAggregate.Create("email@email.com", "123456", employee);
            string newPassword = "654321";

            loginEntity.ChangePassword("123456", newPassword);

            loginEntity.Password.Should().Be(newPassword);
        }

        [Theory]
        [InlineData("", "The parameter Email is required.")]
        [InlineData(" ", "The parameter Email is required.")]
        [InlineData(null, "The parameter Email is required.")]
        [InlineData("@email", "The parameter Email is invalid.")]
        public void SetEmail_Should_Fail(string email, string errorMessage)
        {
            var employee = EmployeeAggregate.Create("name", "role");
            var loginEntity = LoginAggregate.Create("email@email.com", "123456", employee);

            var action = () => loginEntity.SetEmail(email);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Fact]
        public void SetEmail_Should_Change_Email()
        {
            var employee = EmployeeAggregate.Create("name", "role");
            var loginEntity = LoginAggregate.Create("email@email.com", "123456", employee);
            var newEmail = "email2@email.com";

            loginEntity.SetEmail(newEmail);

            loginEntity.Email.Should().Be(newEmail);
        }
    }
}
