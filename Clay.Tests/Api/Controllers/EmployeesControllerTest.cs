using Clay.Api.Middlewares;
using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Interfaces.Services;
using Clay.Domain.Aggregates.Employee;
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
            var client = CreateTestServerClient(fakeService);

            var response = await client.GetAsync("api/employees");

            var result = await response.Content.ReadFromJsonAsync<List<EmployeeResponse>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetById_Should_Return_Data()
        {
            var employee = PrepareEmployeeData();
            var fakeService = A.Fake<IEmployeeService>();
            A.CallTo(() => fakeService.GetById(A<int>.Ignored)).Returns(employee);
            var client = CreateTestServerClient(fakeService);

            var response = await client.GetAsync("api/employees/1");

            var result = await response.Content.ReadFromJsonAsync<EmployeeResponse>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Id.Should().Be(employee.Id);
        }

        [Fact]
        public async Task Post_Should_Create_Data()
        {
            var employee = PrepareEmployeeData().Adapt<EmployeeRequest>();
            var fakeService = A.Fake<IEmployeeService>();
            var client = CreateTestServerClient(fakeService);

            var response = await client.PostAsJsonAsync("api/employees", employee);

            var a = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Put_Should_Update_Data()
        {
            var employee = PrepareEmployeeData().Adapt<EmployeeRequest>();
            var fakeService = A.Fake<IEmployeeService>();
            var client = CreateTestServerClient(fakeService);

            var response = await client.PutAsJsonAsync("api/employees/1", employee);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Should_Remove_Data()
        {
            var fakeService = A.Fake<IEmployeeService>();
            var client = CreateTestServerClient(fakeService);

            var response = await client.DeleteAsync("api/employees/1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
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

        private HttpClient CreateTestServerClient(IEmployeeService fakeService)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(fakeService);

                        builder.Configure(app => app.UseMiddleware<ExceptionMiddleware>());
                    });
                });

            var client = application.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            return client;
        }
    }
}
