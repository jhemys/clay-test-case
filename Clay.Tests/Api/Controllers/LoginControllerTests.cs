using Clay.Api.Controllers;
using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Clay.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Clay.Tests.Api.Controllers
{
    public class LoginControllerTests
    {
        [Fact]
        public async Task Authenticate_Should_Fail_With_Invalid_Request()
        {
            var fakeService = A.Fake<IEmployeeService>();
            var client = CreateTestServerClient(fakeService);
            A.CallTo(() => fakeService.GetByEmailAndPassword(A<string>.Ignored, A<string>.Ignored)).Throws<EntityNotFoundException>();
            var loginController = new LoginController(fakeService);

            var request = new LoginRequest
            {
                Email = "email@email.com",
                Password = "123456"
            };

            var response = await client.PostAsJsonAsync("/api/Login", request);

            var result = await response.Content.ReadFromJsonAsync<ErrorDetails>();

            result.Message.Should().Be("Email/Password incorrect or invalid.");
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Authenticate_Should_Fail_Unexpected_Error()
        {
            var fakeService = A.Fake<IEmployeeService>();
            var client = CreateTestServerClient(fakeService);
            A.CallTo(() => fakeService.GetByEmailAndPassword(A<string>.Ignored, A<string>.Ignored)).Throws<Exception>();
            var request = new LoginRequest
            {
                Email = "email@email.com",
                Password = "123456"
            };

            var response = await client.PostAsJsonAsync("/api/Login", request);

            var result = await response.Content.ReadFromJsonAsync<ErrorDetails>();

            result.Message.Should().Be("Internal Server Error");
            result.StatusCode.Should().Be(500);
        }



        [Fact]
        public async Task Authenticate_Should_Return_Token()
        {
            var fakeService = A.Fake<IEmployeeService>();
            A.CallTo(() => fakeService.GetByEmailAndPassword(A<string>.Ignored, A<string>.Ignored));
            A.CallTo(() => fakeService.GenerateToken(A<EmployeeDTO>.Ignored)).Returns("123456");
            var client = CreateTestServerClient(fakeService);
            var request = new LoginRequest
            {
                Email = "email@email.com",
                Password = "123456"
            };

            var response = await client.PostAsJsonAsync("/api/Login", request);

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            result.Token.Should().Be("123456");
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

            var client = application.CreateClient();

            return client;
        }
    }
}
