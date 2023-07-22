namespace Immobilier.Host.Requests
{
    public record EditUserRequest(ulong Id, string Name, string Email, uint Age);
}
