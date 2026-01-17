using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Application.Interfaces.Super
{
    public interface ICrudService<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task<T> AddAsync(T dto);
        Task<T?> UpdateAsync(T dto);
        Task RemoveAsync(long id);

    }
}
