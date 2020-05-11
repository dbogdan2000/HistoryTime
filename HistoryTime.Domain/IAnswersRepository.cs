using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IAnswersRepository
    {
        IEnumerable<Answer> Get();
        
        Answer Get(int id);
        
        Answer Get(string text);
        
        Answer Get(bool isCorrect);

        Question GetQuestion(int questionId);

        ICollection<UserAnswer> GetUsersAnswers(int id);

        void Create(Answer answer);

        void Delete(int id);
    }
}