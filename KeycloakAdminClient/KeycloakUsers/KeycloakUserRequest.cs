namespace KeycloakAdminClient;

public abstract class KeycloakUserRequest
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool? EmailVerified { get; set; }
    /// <summary>
    /// Lista de ações obrigatórias que podem ser atribuídas ao usuário no Keycloak.
    /// Veja <see cref="RequiredActionsConsts"/> para os valores disponíveis.
    /// </summary>
    public List<string>? RequiredActions { get; set; }
}

public abstract class KeycloakUserRequest<TAttributes> : KeycloakUserRequest
{
    public TAttributes? Attributes { get; set; }
}

public class KeycloakUserCreateRequest<TAttributes> : KeycloakUserRequest<TAttributes>
{
    public required string Username { get; set; }
    public bool Enabled { get; set; } = true;
}

public class KeycloakUserCreateRequest : KeycloakUserCreateRequest<Array>
{
}

public class KeycloakUserUpdateRequest<TAttributes> : KeycloakUserRequest<TAttributes>
{
    public string? Username { get; set; }
    public bool? Enabled { get; set; }
}

public class KeycloakUserUpdateRequest : KeycloakUserUpdateRequest<Array>
{
}

public static class RequiredActionsConsts
{
    public const string VERIFY_EMAIL = "VERIFY_EMAIL";
    public const string UPDATE_PASSWORD = "UPDATE_PASSWORD";
    public const string UPDATE_EMAIL = "UPDATE_EMAIL";
    public const string UPDATE_PROFILE = "UPDATE_PROFILE";
    public const string CONFIGURE_TOTP = "CONFIGURE_TOTP";
    public const string RECOVERY_AUTHENTICATION_CODES = "RECOVERY_AUTHENTICATION_CODES";
    public const string WEBAUTHN_REGISTER = "WEBAUTHN_REGISTER";
    public const string WEBAUTHN_REGISTER_PASSWORDLESS = "WEBAUTHN_REGISTER_PASSWORDLESS";
    public const string DELETE_CREDENTIAL = "DELETE_CREDENTIAL";
}