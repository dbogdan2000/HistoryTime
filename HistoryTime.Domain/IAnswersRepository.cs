using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IAnswersRepository
    {
        IEnumerable<Answer> Get();
        
        Answer Get(int id);

        ICollection<UserAnswer> GetUsersAnswers(int id);

        ICollection<AnswerTheQuestion> GetAnswerTheQuestions(int id);

        void Create(Answer answer);

        void Delete(int id);
    }
}