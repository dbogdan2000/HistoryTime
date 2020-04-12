namespace HistoryTime.Domain
{
    public interface IUsersRepository
    {
        User[] Get();

        User Get(int id);

        User Get(string name);

        void Create(User user);

        void Delete(int id);
    }
}