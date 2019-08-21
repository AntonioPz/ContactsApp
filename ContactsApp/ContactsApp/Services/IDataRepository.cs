using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApp.Services
{
    public interface IDataRepository<T>
    {
        Task<int> AddAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(T item);
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();

    }
}
