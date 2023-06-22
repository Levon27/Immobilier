using Immobilier.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immobilier.DataAccess.Repository.Contracts
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAll();
        Task<Property?> GetById(ulong id);
        ulong CreateProperty(Property property);
    }
}
