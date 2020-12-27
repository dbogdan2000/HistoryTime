using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public interface IRolesRepository
    {
        IEnumerable<Role> Get();

        Role Get(int id);

        void Create(Role role);

        void Delete(int id);
    }
}