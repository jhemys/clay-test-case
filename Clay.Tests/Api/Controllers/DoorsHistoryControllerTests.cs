using Clay.Api.Models;
using Clay.Application.Interfaces.Services;
using Clay.Domain.Aggregates.DoorHistory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Clay.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Clay.Tests.Api.Controllers
{
    public class DoorsHistoryControllerTests
    {
        [Fact]
        public async Task GetAllAccessHistory_Should_Return_Data()
        {
            var doorsHistories = PrepareData();
            var fakeService = A.Fake<IDoorHistoryService>();
            A.CallTo(() => fakeService.GetAllPaged(A<int?>.Ignored, A<int?>.Ignored)).Returns(doorsHistories);
            var client = CreateTestServerClient(fakeService);

            var response = await client.GetAsync("api/doors/access-history");

            var result = await response.Content.ReadFromJsonAsync<PagedResultSerializable<DoorHistoryResponse>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Items.Count().Should().Be(2);

        }

        [Fact]
        public async Task GetAllAccessHistoryByDoor_Should_Return_Data()
        {
            var doorsHistories = PrepareData();
            var fakeService = A.Fake<IDoorHistoryService>();
            A.CallTo(() => fakeService.GetByDoorIdPaged(A<int>.Ignored, A<int?>.Ignored, A<int?>.Ignored)).Returns(doorsHistories);
            var client = CreateTestServerClient(fakeService);

            var response = await client.GetAsync("api/doors/1/access-history");

            var result = await response.Content.ReadFromJsonAsync<PagedResultSerializable<DoorHistoryResponse>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Items.Count().Should().Be(2);

        }

        private (List<DoorHistory> doorHistories, int total) PrepareData()
        {
            var doorHistories = new List<DoorHistory>
            {
                DoorHistory.Create(1, "Test", 1, "Employee", "Unlocked", DateTime.UtcNow, null),
                DoorHistory.Create(1, "Test", 2, "Employee 2", "UnlockFailed", DateTime.UtcNow, null, "error")
            };

            return (doorHistories, 2);
        }

        private HttpClient CreateTestServerClient(IDoorHistoryService fakeService)
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
