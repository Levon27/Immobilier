namespace Immobilier.Host.Requests
{
    public record EditPropertyRequest(ulong Id, string Name, string Address);
}
