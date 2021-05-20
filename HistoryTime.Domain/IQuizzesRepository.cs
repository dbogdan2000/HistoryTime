using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IQuizzesRepository : IRepository<Quiz>
    {
        Task<IEnumerable<Question>> GetQuestions(int id);
        
    }
}