using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Repositories;
using Clay.Application.Services;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.DomainObjects.Exceptions;

namespace Clay.Tests.Application.Services
{
    public class EmployeeServiceTests
    {
        [Fact]
        public async Task GetAll_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var employees = PrepareEmployeesData();
            A.CallTo(() => unitOfWork.EmployeeRepository.GetAll()).Returns(employees);

            var data = await service.GetAll();

            data.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetById_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var employee = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employee);

            var data = await service.GetById(1);

            data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var entity = PrepareEmployeeData(true);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(entity);

            var action = () => service.GetById(1);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
        }

        [Fact]
        public async Task UpdateEmployee_Should_Fail_With_Invalid_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var employee = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employee);
            var employeeDto = new EmployeeDTO()
            {
                Email = "email@email.com",
                Password = "password",
                Role = "Role",
            };

            var action = async () => await service.UpdateEmployee(employeeDto);

            await action.Should().ThrowAsync<DomainException>().WithMessage("The parameter Name is required.");
            A.CallTo(() => unitOfWork.EmployeeRepository.Update(A<Employee>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateEmployee_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var entity = PrepareEmployeeData(true);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(entity);
            var employee = new EmployeeDTO()
            {
                Email = "email@email.com",
                Password = "password",
                Role = "Role",
            };

            var action = async () => await service.UpdateEmployee(employee);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
            A.CallTo(() => unitOfWork.EmployeeRepository.Update(A<Employee>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateEmployee_Should_Update_Employee()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var employee = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employee);
            var employeeDto = new EmployeeDTO()
            {
                Name = "Test",
                Email = "email@email.com",
                Password = "password",
                Role = "Role",
            };

            await service.UpdateEmployee(employeeDto);

            employee.Name.Should().Be("Test");
            A.CallTo(() => unitOfWork.EmployeeRepository.Update(A<Employee>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteEmployee_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var entity = PrepareEmployeeData(true);
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(entity);

            var action = async () => await service.DeleteEmployee(1);

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
            A.CallTo(() => unitOfWork.EmployeeRepository.Update(A<Employee>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task DeleteEmployee_Should_Update_Employee()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new EmployeeService(unitOfWork);
            var employee = PrepareEmployeeData();
            A.CallTo(() => unitOfWork.EmployeeRepository.GetById(A<int>._)).Returns(employee);

            await service.DeleteEmployee(1);

            employee.IsActive.Should().BeFalse();
            A.CallTo(() => unitOfWork.EmployeeRepository.Update(A<Employee>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        private List<Employee> PrepareEmployeesData()
        {
            return new List<Employee>
            {
                Employee.Create("Name", "Role")
            };

        }

        private Employee? PrepareEmployeeData(bool createNullObject = false)
        {
            if (createNullObject)
                return null;

            return Employee.Create("Name", "Role");
        }
    }
}
