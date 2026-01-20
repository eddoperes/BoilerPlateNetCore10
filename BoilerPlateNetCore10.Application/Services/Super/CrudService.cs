using BoilerPlateNetCore10.Application.Interfaces.Super;
using BoilerPlateNetCore10.Domain.Entities.Super;
using BoilerPlateNetCore10.Domain.Interfaces.Super;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Application.Services.Super
{
    public class CrudService<T, G> : ICrudService<T> where T : class where G : Entity
    {

        private readonly IGenericRepository<G> _repository;

        public CrudService(IGenericRepository<G> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IGenericRepository<G>));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Adapt<IEnumerable<T>>();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item.Adapt<T>();
        }

        public async Task<T> AddAsync(T dto)
        {
            var item = dto.Adapt<G>();
            var newDTO = (await _repository.CreateAsync(item)).Adapt<T>();
            return newDTO;
        }

        public async Task<T?> UpdateAsync(T dto)
        {
            var item = dto.Adapt<G>();
            return (await _repository.UpdateAsync(item)).Adapt<T>();
        }

        public async Task RemoveAsync(long id)
        {
            await _repository.RemoveAsync(id);
        }

    }
}
