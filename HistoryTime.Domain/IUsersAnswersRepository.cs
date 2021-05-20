using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IUsersAnswersRepository
    {
        Task<IEnumerable<UserAnswer>> GetAll();

        Task<UserAnswer> Get(int userId, int answerId, int questionId);

        Task<User> GetUser(int userId);

        Task<Answer> GetAnswer(int answerId);

        Task<Question> GetQuestion(int questionId);

        Task Create(UserAnswer userAnswer);

        Task Delete(int answerId, int userId, int questionId);

    }
}