using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.Interfaces.Super;
using BoilerPlateNetCore10.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlateNetCore10.Infra.Data.Repository.Super
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {

        protected readonly ApplicationDbContext _applicationDbContext;
        protected DbSet<T> _dataSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
            _dataSet = _applicationDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var items = await _dataSet.ToListAsync();
            return items;
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dataSet.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<T> CreateAsync(T item)
        {
            _applicationDbContext.Add(item);
            await _applicationDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _applicationDbContext.Update(item);
            var i = await _applicationDbContext.SaveChangesAsync();
            return item;
        }

        public async Task RemoveAsync(long id)
        {
            var item = _dataSet.SingleOrDefault(p => p.Id.Equals(id));
            if (item != null)
            {
                _applicationDbContext.Remove(item);
                await _applicationDbContext.SaveChangesAsync();
            }
        }

        public bool Exists(long id)
        {
            return _dataSet.Any(i => i.Id.Equals(id));
        }
    }

}