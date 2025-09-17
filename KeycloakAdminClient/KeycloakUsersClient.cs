using System.Net.Http.Json;
using System.Text.Json;

namespace KeycloakAdminClient;

public class KeycloakUsersClient(HttpClient httpClient)
{
    public async Task<IEnumerable<KeycloakUser>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("users", cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<IEnumerable<KeycloakUser>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            },
            cancellationToken
        );

        return users ?? Enumerable.Empty<KeycloakUser>();
    }
}

public class KeycloakUser
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool Enabled { get; set; }
    public IEnumerable<string>? RealmRoles { get; set; }
}