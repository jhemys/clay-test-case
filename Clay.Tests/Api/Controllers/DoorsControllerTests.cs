using Clay.Api.Models;
using Clay.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Clay.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Clay.Application.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Clay.Api.Controllers;
using Mapster;
using Clay.Domain.Aggregates.Door;
using Castle.Core.Internal;
using System.Security.Claims;
using System.Security.Principal;
using Azure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

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
            var controller = new DoorsController(fakeService);

            var result = await controller.GetAll();

            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetById_Should_Return_Data()
        {
            var door = PrepareDoorData();
            var fakeService = A.Fake<IDoorService>();
            A.CallTo(() => fakeService.GetById(A<int>.Ignored)).Returns(door);
            var controller = new DoorsController(fakeService);

            var result = await controller.GetById(1);

            result.Id.Should().Be(door.Id);
        }

        [Fact]
        public async Task Post_Should_Create_Data()
        {
            var door = PrepareDoorData();
            var fakeService = A.Fake<IDoorService>();
            var controller = new DoorsController(fakeService);

            await controller.Post(door.Adapt<DoorRequest>());

            A.CallTo(() => fakeService.CreateDoor(A<DoorDTO>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task Put_Should_Update_Data()
        {
            var door = PrepareDoorData();
            var fakeService = A.Fake<IDoorService>();
            var controller = new DoorsController(fakeService);

            await controller.Put(1, door.Adapt<DoorRequest>());

            A.CallTo(() => fakeService.UpdateDoor(A<DoorDTO>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task Delete_Should_Remove_Data()
        {
            var fakeService = A.Fake<IDoorService>();
            var controller = new DoorsController(fakeService);

            await controller.Delete(1);

            A.CallTo(() => fakeService.DeleteDoor(A<int>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task AddRole_Should_Update_Data()
        {
            var role = PrepareRoleData();
            var fakeService = A.Fake<IDoorService>();
            var controller = new DoorsController(fakeService);

            await controller.AddRole(1, role.Adapt<RoleRequest>());

            A.CallTo(() => fakeService.AddAllowedRole(A<int>.Ignored, A<RoleDTO>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task RemoveRole_Should_Update_Data()
        {
            var role = PrepareRoleData();
            var fakeService = A.Fake<IDoorService>();
            var controller = new DoorsController(fakeService);

            await controller.RemoveRole(1, role.Adapt<RoleRequest>());

            A.CallTo(() => fakeService.RemoveAllowedRole(A<int>.Ignored, A<RoleDTO>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task UnlockDoor_Should_Unlock_Door()
        {
            var fakeService = A.Fake<IDoorService>();

            var controller = new DoorsController(fakeService);
            AddUserInfoIntoContext(controller, "1");

            await controller.Unlock(1);

            A.CallTo(() => fakeService.UnlockDoor(A<int>.Ignored, A<int>.Ignored, A<string?>.Ignored)).MustHaveHappened();
        }

        [Theory]
        [InlineData("")]
        [InlineData("Text")]
        public async Task UnlockDoor_Should_Fail(string idValue)
        {
            var fakeService = A.Fake<IDoorService>();

            var controller = new DoorsController(fakeService);
            AddUserInfoIntoContext(controller, idValue);

            await controller.Unlock(1);

            A.CallTo(() => fakeService.UnlockDoor(A<int>.Ignored, A<int>.Ignored, A<string?>.Ignored)).MustNotHaveHappened();
        }

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

        private void AddUserInfoIntoContext(DoorsController controller, string idValue)
        {
            controller.ControllerContext = new ControllerContext();
            var request = new HttpRequestFeature();
            var features = new FeatureCollection();
            features.Set<IHttpRequestFeature>(request);
            controller.ControllerContext.HttpContext = new DefaultHttpContext(features)
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", idValue),
                }))
            };
        }
    }
}
