using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RCARS.Interface.Tests
{
    public class VehiclesControllerShould
    {
        [SetUp]
        public async Task Setup()
        {
        }

        [Test]
        public async Task ReturnAllVehicles()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetStringAsync("/Vehicles");

            var vehicles = response.Split("},{");
            vehicles.Should().HaveCount(3);
            vehicles[0].Should().Contain("\"numberPlate\":\"1548HKP\"");
            vehicles[1].Should().Contain("\"numberPlate\":\"4567KDF\"");
            vehicles[2].Should().Contain("\"numberPlate\":\"8511KXK\"");

        }

        [Test]
        public async Task ReturnVehiclesOfCustomer()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var customerId = "2830D638-073F-49D8-9F95-19D41ADE427E";

            var response = await client.GetStringAsync($"/Customers/{customerId}/vehicles");

            var vehicles = response.Split("},{");
            vehicles.Should().HaveCount(2);
            vehicles[0].Should().Contain("\"numberPlate\":\"4567KDF\"");
            vehicles[1].Should().Contain("\"numberPlate\":\"1548HKP\"");
        }

        [Test]
        public async Task ReturnCustomerFromEmail()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var userEmail = "2830D638-073F-49D8-9F95-19D41ADE427E";

            var response = await client.GetStringAsync($"/Customer/{userEmail}");

            response.Should().Contain("\"numberPlate\":\"4567KDF\"");
        }
    }
}