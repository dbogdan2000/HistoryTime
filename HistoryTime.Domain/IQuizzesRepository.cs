using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IQuizzesRepository
    {
        IEnumerable<Quiz> Get();

        Quiz Get(int id);

        IEnumerable<Question> GetQuestions(int id);

        void Create(Quiz quiz);

        void Delete(int id);
    }
}