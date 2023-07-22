using Immobilier.Domain;

namespace Immobilier.Infrastructure.Auth.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
