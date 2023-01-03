using Clay.Api.Controllers;
using Clay.Api.Middlewares;
using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Clay.Tests.Api.Controllers
{
    public class EmployeesControllerTest
    {
        [Fact]
        public async Task GetAll_Should_Return_Data()
        {
            var employees = PrepareEmployeesData();
            var fakeService = A.Fake<IEmployeeService>();
            A.CallTo(() => fakeService.GetAll()).Returns(employees);
            var controller = new EmployeesController(fakeService);

            var response = await controller.GetAll();
            response.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetById_Should_Return_Data()
        {
            var employee = PrepareEmployeeData();
            var fakeService = A.Fake<IEmployeeService>();
            A.CallTo(() => fakeService.GetById(A<int>.Ignored)).Returns(employee);
            var controller = new EmployeesController(fakeService);

            var response = await controller.GetById(1);

            response.Id.Should().Be(employee.Id);
        }

        [Fact]
        public async Task Put_Should_Update_Data()
        {
            var employee = PrepareEmployeeData().Adapt<EmployeeRequest>();
            var fakeService = A.Fake<IEmployeeService>();
            var controller = new EmployeesController(fakeService);

            await controller.Put(1, employee);

            A.CallTo(() => fakeService.UpdateEmployee(A<EmployeeDTO>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task Delete_Should_Remove_Data()
        {
            var fakeService = A.Fake<IEmployeeService>();
            var controller = new EmployeesController(fakeService);

            await controller.Delete(1);

            A.CallTo(() => fakeService.DeleteEmployee(A<int>.Ignored)).MustHaveHappened();
        }

        private List<EmployeeDTO> PrepareEmployeesData()
        {
            return new List<EmployeeDTO>
            {
                new EmployeeDTO
                {
                    Id = 1,
                    Name = "Test",
                    Email = "email@test.com",
                    IsActive = true,
                    Password = "password",
                    Role = "Admin"
                },
                new EmployeeDTO
                {
                    Id = 2,
                    Name = "Test 2",
                    Email = "email2@test.com",
                    IsActive = true,
                    Password = "password2",
                    Role = "Admin"
                }
            };
        }

        private EmployeeDTO PrepareEmployeeData(bool createNullObject = false)
        {
            return new EmployeeDTO
            {
                Name = "Test",
                Email = "email@test.com",
                IsActive = true,
                Password = "password",
                Role = "Admin"
            };
        }
    }
}
