using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IRolesRepository
    {
        IEnumerable<Role> Get();

        Role Get(int id);

        Role Get(string name);

        void Create(Role role);

        void Delete(int id);
    }
}