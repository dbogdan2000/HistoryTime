using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IUsersRepository
    {
        IEnumerable<User> Get();

        User Get(int id);

        User Get(string name);

        IEnumerable<UserAnswer> GetUserAnswers(int id);

        void Create(User user);

        void Delete(int id);
    }
}