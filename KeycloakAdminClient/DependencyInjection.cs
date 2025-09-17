using Duende.AccessTokenManagement;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAdminClient;

public static class DependencyInjection
{
    public static IServiceCollection AddKeycloakAdminClient(this IServiceCollection services)
    {
        var keycloakAdminClient = ClientCredentialsClientName.Parse("keycloak-admin.client");
        services.AddClientCredentialsTokenManagement()
              .AddClient(keycloakAdminClient, client =>
              {
                  client.TokenEndpoint = new Uri("http://localhost:1100/realms/bora/protocol/openid-connect/token");
                  client.ClientId = ClientId.Parse("bora-client");
                  client.ClientSecret = ClientSecret.Parse("AQXrW4Inbt8RyuMHxASqXvNNmZGTAn7V");
                  client.Scope = Scope.Parse("managed-users");
              });


        services.AddClientCredentialsHttpClient("users",
          keycloakAdminClient,
          client =>
          {
              client.BaseAddress = new Uri("http://localhost:1100/admin/realms/bora/users");
          });

        //services.AddHttpClient<KeycloakUsersClient>(client =>
        //{
        //    client.BaseAddress = new Uri("http://localhost:1100/admin/realms/bora/users");
        //})
        //.AddClientCredentialsTokenHandler(keycloakAdminClient);

        return services;
    }
}