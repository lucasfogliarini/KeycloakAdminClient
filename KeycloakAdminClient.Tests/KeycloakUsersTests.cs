using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAdminClient.Tests;

public class KeycloakUsersTests
{
    private readonly KeycloakUsersClient _keycloakUsersClient;

    public KeycloakUsersTests()
    {
        var keycloakAdminConfig = new KeycloakAdminConfig
        {
            KeycloakServer = "http://localhost:1100/",
            Realm = "bora",
            ClientId = "bora-client",
            ClientSecret = "AQXrW4Inbt8RyuMHxASqXvNNmZGTAn7V"
        };

        var services = new ServiceCollection();
        services.AddKeycloakAdminClient(keycloakAdminConfig);
        var provider = services.BuildServiceProvider();

        _keycloakUsersClient = provider.GetRequiredService<KeycloakUsersClient>();
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturn_AtLeastOneUser()
    {
        // Act
        var users = await _keycloakUsersClient.GetUsersAsync();

        // Assert
        Assert.NotNull(users);
        Assert.NotEmpty(users);

        var firstUser = users.First();

        Assert.False(string.IsNullOrWhiteSpace(firstUser.Id));
        Assert.False(string.IsNullOrWhiteSpace(firstUser.Username));
        Assert.False(string.IsNullOrWhiteSpace(firstUser.Email));
    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturn_AtLeastOneUser()
    {
        // Act
        var user = await _keycloakUsersClient.GetUserByEmailAsync("bora.reunir@sample.com");

        // Assert
        Assert.NotNull(user);

        Assert.False(string.IsNullOrWhiteSpace(user.Id));
        Assert.False(string.IsNullOrWhiteSpace(user.Username));
        Assert.False(string.IsNullOrWhiteSpace(user.Email));
    }

    [Fact]
    public async Task GetUserByUsernameAsync_ShouldReturn_AtLeastOneUser()
    {
        // Act
        var user = await _keycloakUsersClient.GetUserByUsernameAsync("bora");

        // Assert
        Assert.NotNull(user);

        Assert.False(string.IsNullOrWhiteSpace(user.Id));
        Assert.False(string.IsNullOrWhiteSpace(user.Username));
        Assert.False(string.IsNullOrWhiteSpace(user.Email));
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        // Arrange
        var newUser = new KeycloakUserCreateRequest
        {
            Username = "bora2",
            Email = "bora.reunir@sample.com",
            FirstName = "Bora",
            LastName = "Reunir",
            Enabled = true,
            EmailVerified = true,
        };

        // Act
        await _keycloakUsersClient.CreateUserAsync(newUser);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldUpdateUser()
    {
        // Arrange
        var user = new KeycloakUserUpdateRequest
        {
            FirstName = "Bora Atualizado",
            LastName = "Reunir Atualizado",
            Enabled = true,
            EmailVerified = true,
        };

        // Act
        await _keycloakUsersClient.UpdateUserAsync("98447fae-5137-4314-84aa-c6a66aa3348a", user);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser_WithAttributes()
    {
        // Arrange
        var newUser = new KeycloakUserCreateRequest<UserAttributes>
        {
            Username = "bora",
            Email = "bora.reunir@sample.com",
            FirstName = "Bora",
            LastName = "Reunir",
            Enabled = true,
            EmailVerified = true,
            Attributes = new UserAttributes
            {
                Empresa = "Empresa1"
            }
        };

        // Act
        await _keycloakUsersClient.CreateUserAsync(newUser);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldUpdateUser_WithAttributes()
    {
        // Arrange
        var user = new KeycloakUserUpdateRequest<UserAttributes>
        {
            Attributes = new UserAttributes
            {
                Empresa = "EmpresaAtualizada"
            }
        };

        // Act
        await _keycloakUsersClient.UpdateUserAsync("98447fae-5137-4314-84aa-c6a66aa3348a", user);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        // Arrange
        var userId = "cbdac729-1eed-456f-bbcd-523f9337220a";

        // Act
        await _keycloakUsersClient.DeleteUserAsync(userId);
    }
}