using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IQuestionsRepository : IRepository<Question>
    {
        Task<Quiz> GetQuiz(int quizId);

        Task<IEnumerable<AnswerTheQuestion>> GetAnswersTheQuestion(int id);

        Task<IEnumerable<UserAnswer>> GetUsersAnswers(int id);
    }
}