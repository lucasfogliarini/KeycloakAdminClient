using Duende.AccessTokenManagement;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAdminClient;

public static class DependencyInjection
{
    public static IServiceCollection AddKeycloakAdminClient(this IServiceCollection services, KeycloakAdminConfig keycloakAdminConfig)
    {
        var keycloakAdminClient = ClientCredentialsClientName.Parse("keycloak-admin.client");
        keycloakAdminConfig.KeycloakServer = keycloakAdminConfig.KeycloakServer.TrimEnd('/');
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(keycloakAdminClient, client =>
            {
                client.TokenEndpoint = new Uri($"{keycloakAdminConfig.KeycloakServer}/realms/{keycloakAdminConfig.Realm}/protocol/openid-connect/token");
                client.ClientId = ClientId.Parse(keycloakAdminConfig.ClientId);
                client.ClientSecret = ClientSecret.Parse(keycloakAdminConfig.ClientSecret);
                client.Scope = keycloakAdminConfig.Scope == null ? null : Scope.Parse(keycloakAdminConfig.Scope);
            });

        services.AddHttpClient<KeycloakUsersClient>(client =>
        {
            client.BaseAddress = new Uri($"{keycloakAdminConfig.KeycloakServer}/admin/realms/{keycloakAdminConfig.Realm}/users");
        })
        .AddClientCredentialsTokenHandler(keycloakAdminClient);

        return services;
    }
}