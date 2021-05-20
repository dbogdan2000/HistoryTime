using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryTime.Domain
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<IEnumerable<UserAnswer>> GetUserAnswers(int id);

        Task<Role> GetRole(int roleId);
    }
}