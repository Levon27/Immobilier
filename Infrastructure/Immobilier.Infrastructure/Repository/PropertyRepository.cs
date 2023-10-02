using Immobilier.Domain;
using Immobilier.Infrastructure.Config;
using Immobilier.Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Immobilier.Infrastructure.Repository
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

        public uint CreateProperty(Property property)
        {
            var created = _context.Properties.Add(property).Entity;
            _context.SaveChanges();
            return created.Id;
        }

        public async Task<ulong> DeleteProperty(uint id)
        {
            var toRemove = await _context.Properties.SingleOrDefaultAsync(p => p.Id == id);
            _context.Properties.Remove(toRemove);
            _context.SaveChanges();
            return id;
        }
    }
}
