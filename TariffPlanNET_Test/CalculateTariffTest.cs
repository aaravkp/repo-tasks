using System;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using TariffPlanNET;
using TariffPlanNET.Services;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace TariffPlanNET_Test
{
    public class CalculateTariffTest
    {
        private readonly HttpClient _client;

        public CalculateTariffTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET", 3500)]
        public async Task TariffPlanTest(string method, int id)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/tariff/{id}");
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

          
        }

        [Theory]
        [InlineData("GET", 6000)]
        public async Task TariffPlanfWithOtherValTest(string method, int id)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/tariff/{id}");
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Theory]
        [InlineData("GET", "test")]
        public async Task TariffPlanBedRequesttest(string method, string id)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/tariff/{id}");
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


        }

        [Fact]
        public void ServiceTariffPlanTest()
        {
            var ct = new CalculateTariff();
            var tp1 = ct.TariffPlan("3500");
            var basic = tp1.Where(a => a.TariffName== "Basic electricity tariff")
                     .Select(a => a.AnnualCosts)
                     .First();

            Assert.Equal("830,00 €", basic);

            var packaged = tp1.Where(a => a.TariffName == "Packaged tariff")
                     .Select(a => a.AnnualCosts)
                     .First();

            Assert.Equal("800,00 €", packaged);

           
            var tp2 = ct.TariffPlan("6000");
            var basic1 = tp2.Where(a => a.TariffName == "Basic electricity tariff")
                     .Select(a => a.AnnualCosts)
                     .First();

            Assert.Equal("1.380,00 €", basic1);

            var packaged1 = tp2.Where(a => a.TariffName == "Packaged tariff")
                     .Select(a => a.AnnualCosts)
                     .First();

            Assert.Equal("1.400,00 €", packaged1);


        }
    }
}
