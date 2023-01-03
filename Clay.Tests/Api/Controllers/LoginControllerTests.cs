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
using Microsoft.AspNetCore.Mvc;

namespace Clay.Tests.Api.Controllers
{
    public class LoginControllerTests
    {
        [Fact]
        public async Task Authenticate_Should_Fail_With_Invalid_Request()
        {
            var fakeService = A.Fake<ILoginService>();
            A.CallTo(() => fakeService.GetByEmailAndPassword(A<string>.Ignored, A<string>.Ignored)).Throws<EntityNotFoundException>();
            var controller = new LoginController(fakeService);

            var request = new LoginRequest
            {
                Email = "email@email.com",
                Password = "123456"
            };

            var response = await controller.Authenticate(request);

            var result = (ErrorDetails)((ObjectResult)response.Result).Value;

            result.Message.Should().Be("Email/Password incorrect or invalid.");
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Authenticate_Should_Fail_Unexpected_Error()
        {
            var fakeService = A.Fake<ILoginService>();
            A.CallTo(() => fakeService.GetByEmailAndPassword(A<string>.Ignored, A<string>.Ignored)).Throws<Exception>();
            var controller = new LoginController(fakeService);

            var request = new LoginRequest
            {
                Email = "email@email.com",
                Password = "123456"
            };

            var action = async () => await controller.Authenticate(request);

            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Authenticate_Should_Return_Token()
        {
            var fakeService = A.Fake<ILoginService>();
            A.CallTo(() => fakeService.GetByEmailAndPassword(A<string>.Ignored, A<string>.Ignored));
            A.CallTo(() => fakeService.GenerateToken(A<LoginDTO>.Ignored)).Returns("123456");
            var controller = new LoginController(fakeService);

            var request = new LoginRequest
            {
                Email = "email@email.com",
                Password = "123456"
            };

            var response = await controller.Authenticate(request);

            var result = response.Value;

            result.Token.Should().Be("123456");
        }
    }
}
