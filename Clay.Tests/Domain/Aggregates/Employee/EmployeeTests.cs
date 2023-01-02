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
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", tagIdentification);

            employeeEntity.Should().NotBeNull();
        }

        [Theory]
        [InlineData("", "Role", "tagIdentification", "The parameter Name is required.")]
        [InlineData(" ", "Role", "tagIdentification", "The parameter Name is required.")]
        [InlineData(null, "Role", "tagIdentification", "The parameter Name is required.")]
        [InlineData("Employee Name", "", "tagIdentification", "The parameter Role is required.")]
        [InlineData("Employee Name", " ", "tagIdentification", "The parameter Role is required.")]
        [InlineData("Employee Name", null, "tagIdentification", "The parameter Role is required.")]
        public void Should_Fail_To_Create_Valid_Entity(string employeeName, string roleName, string tagIdentification, string errorMessage)
        {
            var action = () => EmployeeAggregate.Create(employeeName, roleName, tagIdentification);

            action.Should().Throw<DomainException>().WithMessage(errorMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetName_Should_Fail(string name)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "tagId");

            var action = () => employeeEntity.SetName(name);

            action.Should().Throw<DomainException>().WithMessage("The parameter Name is required.");
        }

        [Fact]
        public void SetName_Should_Change_Name()
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "tagId");
            var newName = "New Name";

            employeeEntity.SetName(newName);

            employeeEntity.Name.Should().Be(newName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetRole_Should_Fail(string role)
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "tagId");

            var action = () => employeeEntity.SetRole(role);

            action.Should().Throw<DomainException>().WithMessage("The parameter Role is required.");
        }

        [Fact]
        public void SetRole_Should_Change_Role()
        {
            var employeeEntity = EmployeeAggregate.Create("Employee Name", "Role", "tagId");
            var newRole = "New Role";

            employeeEntity.SetRole(newRole);

            employeeEntity.Role.Should().Be(newRole);
        }
    }
}