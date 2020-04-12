using System.Collections.Generic;
using System.Linq;
using HistoryTime.Domain;

namespace HistoryTime.Data
{
    public class UsersRepository: IUsersRepository
    {
        private readonly ICollection<User> _users = new List<User>();

        public User[] Get()
        {
            return _users.ToArray();
        }

        public User Get(int id)
        {
            foreach (var user in _users)
            {
                if (user.Id == id)
                    return user;
            }

            return null;
        }

        public User Get(string name)
        {
            foreach (var user in _users)
            {
                if (user.Name == name)
                    return user;
            }

            return null;
        }

        public void Create(User user)
        {
            _users.Add(user);
        }

        public void Delete(int id)
        {
            foreach (var user in _users)
            {
                if (user.Id == id)
                {
                    _users.Remove(user);
                    return;
                }
            }             
        }
    }
}