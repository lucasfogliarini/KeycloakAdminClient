namespace KeycloakAdminClient;

public abstract class KeycloakUserRequest
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool? EmailVerified { get; set; }
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
