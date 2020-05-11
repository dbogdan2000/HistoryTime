using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IQuestionsRepository
    {
        IEnumerable<Question> Get();
        
        Question Get(int id);
        
        Question Get(string text);

        Quiz GetQuiz(int quizId);

        IEnumerable<Answer> GetAnswers(int id);

        void Create(Question question);

        void Delete(int id);
    }
}