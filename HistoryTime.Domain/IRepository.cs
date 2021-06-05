using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IRepository<T>  where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Update(T item);
        Task Create(T item);
        Task Delete(int id);
    }
}