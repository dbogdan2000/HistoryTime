using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IAnswersTheQuestionsRepository
    {
        Task<IEnumerable<AnswerTheQuestion>> GetAll();

        Task<AnswerTheQuestion> GetCorrectAnswerOnQuestion(int questionId, bool isCorrect);

        Task<AnswerTheQuestion> Get(int questionId, int answerId);

        Task<Question> GetQuestion(int questionId);

        Task<Answer> GetAnswer(int answerId);

        Task Create(AnswerTheQuestion answerTheQuestion);

        Task Delete(int questionId, int answerId);

    }
}