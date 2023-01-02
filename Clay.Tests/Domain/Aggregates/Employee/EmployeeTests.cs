using Clay.Domain.DomainObjects.Exceptions;
using Xunit.Sdk;
using EmployeeAggregate = Clay.Domain.Aggregates.Employee.Employee;

namespace Clay.Tests.Domain.Aggregates.Employee
{
    public class EmployeeTests
    {
        [Theory]
        [InlineData("tagIdentification")]
        [InlineData(null)]
        public void Should_Create_Valid_Entity(string tagIdentification)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", tagIdentification);

            employeeEntity.Should().NotBeNull();
        }

        [Theory]
        [InlineData("", "Role", "123456", "email@email.com", "tagIdentification", "The parameter Name is required.")]
        [InlineData(" ", "Role", "123456", "email@email.com", "tagIdentification", "The parameter Name is required.")]
        [InlineData(null, "Role", "123456", "email@email.com", "tagIdentification", "The parameter Name is required.")]
        [InlineData("Employee Name", "", "123456", "email@email.com", "tagIdentification", "The parameter Role is required.")]
        [InlineData("Employee Name", " ", "123456", "email@email.com", "tagIdentification", "The parameter Role is required.")]
        [InlineData("Employee Name", null, "123456", "email@email.com", "tagIdentification", "The parameter Role is required.")]
        [InlineData("Employee Name", "Role", "123456", "", "tagIdentification", "The parameter Email is required.")]
        [InlineData("Employee Name", "Role", "123456", " ", "tagIdentification", "The parameter Email is required.")]
        [InlineData("Employee Name", "Role", "123456", null, "tagIdentification", "The parameter Email is required.")]
        [InlineData("Employee Name", "Role", "123456", "@email", "tagIdentification", "The parameter Email is invalid.")]
        [InlineData("Employee Name", "Role", "", "email@email.com", "tagIdentification", "The parameter Password is required.")]
        [InlineData("Employee Name", "Role", " ", "email@email.com", "tagIdentification", "The parameter Password is required.")]
        [InlineData("Employee Name", "Role", null, "email@email.com", "tagIdentification", "The parameter Password is required.")]
        public void Should_Fail_To_Create_Valid_Entity(string employeeName, string roleName, string password, string email, string tagIdentification, string errorMessage)
        {
            var action = () => EmployeeAggregate.Create(employeeName, roleName, password, email, tagIdentification);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Theory]
        [InlineData("123456", "123456", "New Password must be different than Current Password.")]
        [InlineData("654321", "", "Current Password is invalid.")]
        [InlineData("123456", "", "The parameter Password is required.")]
        public void ChangePassword_Should_Fail(string currentPassword, string newPassword, string errorMessage)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");

            var action = () => employeeEntity.ChangePassword(currentPassword, newPassword);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Fact]
        public void ChangePassword_Should_Change_Password()
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");
            string newPassword = "654321";

            employeeEntity.ChangePassword("123456", newPassword);

            employeeEntity.Password.Should().Be(newPassword);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetName_Should_Fail(string name)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");

            var action = () => employeeEntity.SetName(name);

            action.Should().Throw<DomainException>().WithMessage("The parameter Name is required.");
        }

        [Fact]
        public void SetName_Should_Change_Name()
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");
            var newName = "New Name";

            employeeEntity.SetName(newName);

            employeeEntity.Name.Should().Be(newName);
        }

        [Theory]
        [InlineData("", "The parameter Email is required.")]
        [InlineData(" ", "The parameter Email is required.")]
        [InlineData(null, "The parameter Email is required.")]
        [InlineData("@email", "The parameter Email is invalid.")]
        public void SetEmail_Should_Fail(string email, string errorMessage)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");

            var action = () => employeeEntity.SetEmail(email);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Fact]
        public void SetEmail_Should_Change_Email()
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");
            var newEmail = "email2@email.com";

            employeeEntity.SetEmail(newEmail);

            employeeEntity.Email.Should().Be(newEmail);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetRole_Should_Fail(string role)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");

            var action = () => employeeEntity.SetRole(role);

            action.Should().Throw<DomainException>().WithMessage("The parameter Role is required.");
        }

        [Fact]
        public void SetRole_Should_Change_Role()
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "123456", "email@email.com", "tagId");
            var newRole = "New Role";

            employeeEntity.SetRole(newRole);

            employeeEntity.Role.Should().Be(newRole);
        }
    }
}