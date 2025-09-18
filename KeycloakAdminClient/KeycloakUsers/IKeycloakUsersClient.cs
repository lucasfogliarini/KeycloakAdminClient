namespace KeycloakAdminClient;

/// <summary>
/// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_users
/// </summary>
public interface IKeycloakUsersClient
{
    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_get_adminrealmsrealmusers
    /// </summary>
    Task<IEnumerable<KeycloakUserResponse>> GetUsersAsync(string query = "", CancellationToken cancellationToken = default);

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_get_adminrealmsrealmusers
    /// </summary>
    Task<KeycloakUserResponse?> GetUserByEmailAsync(string email, bool exact = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_get_adminrealmsrealmusers
    /// </summary>
    Task<KeycloakUserResponse?> GetUserByUsernameAsync(string username, bool exact = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_post_adminrealmsrealmusers
    /// </summary>
    Task CreateUserAsync<TAttributes>(KeycloakUserCreateRequest<TAttributes> userCreateRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_put_adminrealmsrealmusersuser_id
    /// </summary>
    Task UpdateUserAsync<TAttributes>(Guid userId, KeycloakUserUpdateRequest<TAttributes> userUpdateRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// https://www.keycloak.org/docs-api/latest/rest-api/index.html#_delete_adminrealmsrealmusersuser_id
    /// </summary>
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
}