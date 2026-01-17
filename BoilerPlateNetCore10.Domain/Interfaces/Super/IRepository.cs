using BoilerPlateNetCore10.Domain.Entities.Super;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Domain.Interfaces.Super
{
    public interface IRepository<T> where T : Entity
    {

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(long id);

        Task<T> CreateAsync(T item);

        Task<T> UpdateAsync(T item);

        Task RemoveAsync(long id);

        bool Exists(long id);

    }
}
