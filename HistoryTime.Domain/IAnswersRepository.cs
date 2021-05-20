using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IAnswersRepository : IRepository<Answer>
    {
        Task<ICollection<UserAnswer>>  GetUsersAnswers(int id);

        Task<ICollection<AnswerTheQuestion>> GetAnswerTheQuestions(int id);
        
    }
}