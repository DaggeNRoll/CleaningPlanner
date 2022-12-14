using DataLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IRole
    {
        public IEnumerable<Role> GetAllRoles();
        public Role GetRole(int id);
        public Role GetRole(User user, Room room);
        public IEnumerable<Role> GetRolesWithUsers(Room room);
        public Role GetRoleByUser(int userId);

        public Role UpdateRole(Role role);
        public int DeleteRole(int id);
        public int DeleteRole(Role role);
        public void SaveRole(Role role);

    }
}
