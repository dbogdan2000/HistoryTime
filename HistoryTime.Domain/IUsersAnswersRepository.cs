using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IUsersAnswersRepository
    {
        IEnumerable<UserAnswer> Get();

        UserAnswer Get(int userId, int answerId, int questionId);

        User GetUser(int userId);

        Answer GetAnswer(int answerId);

        Question GetQuestion(int questionId);

        void Create(UserAnswer userAnswer);

        void Delete(int answerId, int userId, int questionId);

    }
}