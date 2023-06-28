using Immobilier.DataAccess.Config;
using Immobilier.DataAccess.Repository.Contracts;
using Immobilier.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immobilier.DataAccess.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            return await _context.Properties.AsNoTracking().ToArrayAsync();
        }

        public async Task<Property?> GetById(ulong id)
        {
            return await _context.Properties.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public ulong CreateProperty(Property property)
        {
            var created = _context.Properties.Add(property).Entity;
            _context.SaveChanges();
            return created.Id;
        }
    }
}
