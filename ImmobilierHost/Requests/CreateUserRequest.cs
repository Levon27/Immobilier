namespace Immobilier.Host.Requests
{
    public record CreateUserRequest(string Name, string Email, string Password, int Age);
}
