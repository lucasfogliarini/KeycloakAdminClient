using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeycloakAdminClient;

/// <summary>
/// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_users
/// </summary>
internal class KeycloakUsersClient(HttpClient httpClient) : IKeycloakUsersClient
{
    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_get_adminrealmsrealmusers
    /// </summary>
    public async Task<IEnumerable<KeycloakUserResponse>> GetUsersAsync(string query = "", CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"?{query}", cancellationToken);
        if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return [];

        if (!response.IsSuccessStatusCode)
            response.EnsureSuccessStatusCode();

        var users = await ReadAsAsync<IEnumerable<KeycloakUserResponse>>(response, cancellationToken);
        return users ?? [];
    }

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_get_adminrealmsrealmusers
    /// </summary>
    public async Task<KeycloakUserResponse?> GetUserByEmailAsync(string email, bool exact = true, CancellationToken cancellationToken = default)
    {
        var users = await GetUsersAsync($"email={email}&exact={exact}", cancellationToken);
        return users.FirstOrDefault();
    }

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_get_adminrealmsrealmusers
    /// </summary>
    public async Task<KeycloakUserResponse?> GetUserByUsernameAsync(string username, bool exact = true, CancellationToken cancellationToken = default)
    {
        var users = await GetUsersAsync($"username={username}&exact={exact}", cancellationToken);
        return users.FirstOrDefault();
    }

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_post_adminrealmsrealmusers
    /// </summary>
    public async Task CreateUserAsync<TAttributes>(KeycloakUserCreateRequest<TAttributes> userCreateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("", userCreateRequest, jsonSerializerOptions, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_put_adminrealmsrealmusersuser_id
    /// </summary>
    public async Task UpdateUserAsync<TAttributes>(Guid userId, KeycloakUserUpdateRequest<TAttributes> userUpdateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"users/{userId}", userUpdateRequest, jsonSerializerOptions,  cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"users/{userId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private async Task<T?> ReadAsAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<T>(jsonSerializerOptions, cancellationToken);
    }
}