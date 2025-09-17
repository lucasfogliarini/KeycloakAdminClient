using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace KeycloakAdminClient.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var services = new ServiceCollection();
            services.AddKeycloakAdminClient();
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<KeycloakUsersClient>();

            var users = await client.GetUsersAsync();
        }

        [Fact]
        public async Task Test2()
        {
            var services = new ServiceCollection();
            services.AddKeycloakAdminClient();
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("users");

            var response = await client.GetAsync("");
            var auth = client.DefaultRequestHeaders.Authorization;
            var responseString = response.Content.ReadAsStringAsync();
        }
    }
}