using System.ComponentModel.DataAnnotations;

namespace KeycloakAdminClient;

public class KeycloakAdminConfig
{
    [Required]
    public required string KeycloakServer { get; set; }
    [Required]
    public required string Realm { get; set; }
    [Required]
    public required string ClientId { get; set; }
    [Required]
    public required string ClientSecret { get; set; }    
    public string? Scope { get; set; }
}
