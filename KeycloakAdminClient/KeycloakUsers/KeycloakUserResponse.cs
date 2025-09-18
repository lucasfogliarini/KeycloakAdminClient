namespace KeycloakAdminClient;

public class KeycloakUserResponse
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool Enabled { get; set; }
    public bool EmailVerified { get; set; }
}
