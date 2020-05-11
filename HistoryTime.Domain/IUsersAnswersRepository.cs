using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IUsersAnswersRepository
    {
        IEnumerable<UserAnswer> Get();

        IEnumerable<UserAnswer> Get(int userId);

        UserAnswer Get(int userId, int answerId);

        User GetUser(int userId);

        Answer GetAnswer(int answerId);

        void Create(UserAnswer userAnswer);

        void Delete(int answerId, int userId);

    }
}