using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAdminClient.Tests;

public class KeycloakUsersTests
{
    private readonly IKeycloakUsersClient _keycloakUsersClient;

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

        _keycloakUsersClient = provider.GetRequiredService<IKeycloakUsersClient>();
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturn_AtLeastOneUser()
    {
        // Act
        var users = await _keycloakUsersClient.GetUsersAsync();

        // Assert
        Assert.NotNull(users);
        Assert.NotEmpty(users);

        var user = users.First();

        Assert.NotNull(user);

        Assert.NotNull(user.Id);
        Assert.NotEmpty(user.Username);
        Assert.NotEmpty(user.Email);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturn_AtLeastOneUser()
    {
        // Act
        var user = await _keycloakUsersClient.GetUserByEmailAsync("bora@mail.com");

        // Assert
        Assert.NotNull(user);

        Assert.NotNull(user.Id);
        Assert.NotEmpty(user.Username);
        Assert.NotEmpty(user.Email);
    }

    [Fact]
    public async Task GetUserByUsernameAsync_ShouldReturn_AtLeastOneUser()
    {
        // Act
        var user = await _keycloakUsersClient.GetUserByUsernameAsync("bora");

        // Assert
        Assert.NotNull(user);

        Assert.NotNull(user.Id);
        Assert.NotEmpty(user.Username);
        Assert.NotEmpty(user.Email);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        // Arrange
        var newUser = new KeycloakUserCreateRequest
        {
            Username = "bora",
            Email = "bora@mail.com",
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
        var userResponse = await _keycloakUsersClient.GetUserByUsernameAsync("bora");
        Assert.NotNull(userResponse);

        var userRequest = new KeycloakUserUpdateRequest
        {
            FirstName = "Bora Atualizado",
            LastName = "Reunir Atualizado",
            Enabled = true,
            EmailVerified = true,
        };

        // Act
        await _keycloakUsersClient.UpdateUserAsync(userResponse.Id, userRequest);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser_WithAttributes()
    {
        // Arrange
        var newUser = new KeycloakUserCreateRequest<UserAttributes>
        {
            Username = "bora_com_atributos",
            Email = "bora_com_atributos@mail.com",
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
        var userResponse = await _keycloakUsersClient.GetUserByUsernameAsync("bora_com_atributos");
        Assert.NotNull(userResponse);

        var user = new KeycloakUserUpdateRequest<UserAttributes>
        {
            Attributes = new UserAttributes
            {
                Empresa = "EmpresaAtualizada"
            }
        };

        // Act
        await _keycloakUsersClient.UpdateUserAsync(userResponse.Id, user);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        // Arrange
        var newUser = new KeycloakUserCreateRequest
        {
            Username = "bora_deletar",
            Email = "bora_deletar@mail.com"
        };
        await _keycloakUsersClient.CreateUserAsync(newUser);
        var userResponse = await _keycloakUsersClient.GetUserByUsernameAsync(newUser.Username);
        Assert.NotNull(userResponse);

        // Act
        await _keycloakUsersClient.DeleteUserAsync(userResponse.Id);
    }
}