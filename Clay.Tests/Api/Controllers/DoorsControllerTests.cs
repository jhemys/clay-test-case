using Clay.Api.Models;
using Clay.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Clay.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Clay.Domain.Aggregates.Employee;
using Clay.Application.DTOs;
using System.Text.Json;
using FluentAssertions.Common;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.Authorization;
using Clay.Api.Controllers;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Clay.Tests.Api.Controllers
{
    public class DoorsControllerTests
    {
        [Fact]
        public async Task GetAll_Should_Return_Data()
        {
            var doors = PrepareDoorList();
            var fakeService = A.Fake<IDoorService>();
            A.CallTo(() => fakeService.GetAll()).Returns(doors);
            var client = CreateTestServerClient(fakeService);

            var response = await client.GetAsync("api/doors");

            var result = await response.Content.ReadFromJsonAsync<List<DoorResponse>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetById_Should_Return_Data()
        {
            var door = PrepareDoorData();
            var fakeService = A.Fake<IDoorService>();
            A.CallTo(() => fakeService.GetById(A<int>.Ignored)).Returns(door);
            var client = CreateTestServerClient(fakeService);

            var response = await client.GetAsync("api/doors/1");

            var result = await response.Content.ReadFromJsonAsync<DoorResponse>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Id.Should().Be(door.Id);
        }

        [Fact]
        public async Task Post_Should_Create_Data()
        {
            var door = PrepareDoorData();
            var fakeService = A.Fake<IDoorService>();
            var client = CreateTestServerClient(fakeService);

            var response = await client.PostAsJsonAsync("api/doors", door);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Put_Should_Update_Data()
        {
            var door = PrepareDoorData();
            var fakeService = A.Fake<IDoorService>();
            var client = CreateTestServerClient(fakeService);

            var response = await client.PutAsJsonAsync("api/doors/1", door);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Should_Remove_Data()
        {
            var fakeService = A.Fake<IDoorService>();
            var client = CreateTestServerClient(fakeService);

            var response = await client.DeleteAsync("api/doors/1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task AddRole_Should_Update_Data()
        {
            var role = PrepareRoleData();
            var fakeService = A.Fake<IDoorService>();
            var client = CreateTestServerClient(fakeService);

            var content = new StringContent(JsonSerializer.Serialize(role), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("api/doors/1/AddRole", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task RemoveRole_Should_Update_Data()
        {
            var role = PrepareRoleData();
            var fakeService = A.Fake<IDoorService>();
            var client = CreateTestServerClient(fakeService);

            var content = new StringContent(JsonSerializer.Serialize(role), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("api/doors/1/RemoveRole", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        //[Fact]
        //public async Task UnlockDoor_Should_Unlock_Door()
        //{
        //    var fakeService = A.Fake<IDoorService>();
        //    var controller = new DoorsController(fakeService);
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, "Nikita"),
        //        new Claim(ClaimTypes.NameIdentifier, "1"),
        //        new Claim("Id", "123456")
        //    };

        //    var identity = new ClaimsIdentity(claims);
        //    var user = new ClaimsPrincipal(identity);

        //    controller.HttpContext = new HttpContext();
        //    controller.HttpContext.User = user;

        //    controller.Unlock(1);

        //    //var door = PrepareDoorData();
        //    //var client = CreateTestServerClient(fakeService);

        //    ////var headers = new Dictionary<string, string>();
        //    ////headers.Add("Authorization", "Bearer 123456");

        //    //var response = await client.PutAsJsonAsync("api/doors/1/unlock", door);

        //    //response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        //}

        private List<DoorDTO> PrepareDoorList()
        {
            return new List<DoorDTO>()
            {
                new DoorDTO
                {
                    Id = 1,
                    Name= "Test",
                    IsLocked = true,
                    IsAccessRestricted = false,
                    Description= "Test",
                    IsActive = true,
                    AllowedRoles = new List<RoleDTO>
                    {
                        new RoleDTO
                        {
                            Name= "Test Role",
                        }
                    }
                },
                new DoorDTO
                {
                    Id = 2,
                    Name= "Test 2",
                    IsLocked = true,
                    IsAccessRestricted = true,
                    Description= "Test 2",
                    AllowedRoles = new List<RoleDTO>
                    {
                        new RoleDTO
                        {
                            Name= "Test Role",
                        }
                    }
                }
            };
        }

        private DoorDTO PrepareDoorData(bool createNullObject = false)
        {
            if (createNullObject)
                return null;

            return new DoorDTO
            {
                Id = 1,
                Name = "Test",
                IsLocked = true,
                IsAccessRestricted = false,
                Description = "Test",
                IsActive = true,
                AllowedRoles = new List<RoleDTO>
                    {
                        new RoleDTO
                        {
                            Name= "Test Role",
                        }
                    }
            };
        }

        private RoleDTO PrepareRoleData()
        {
            return new RoleDTO
            {
                Id = 1,
                Name = "Test"
            };
        }

        private HttpClient CreateTestServerClient(IDoorService fakeService)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(fakeService);

                        builder.Configure(app => app.UseMiddleware<ExceptionMiddleware>());

                        services.AddMvc(options =>
                        {
                            options.Filters.Add(new AllowAnonymousFilter());
                            options.Filters.Add(new FakeUserFilter());
                        })
                        .AddApplicationPart(typeof(Program).Assembly);
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
