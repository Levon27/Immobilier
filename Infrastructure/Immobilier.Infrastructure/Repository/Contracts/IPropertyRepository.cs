using Immobilier.Domain;

namespace Immobilier.Infrastructure.Repository.Contracts
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAll();
        Task<Property?> GetById(ulong id);
        uint CreateProperty(Property property);
        Task<ulong> DeleteProperty(uint id);
    }
}
