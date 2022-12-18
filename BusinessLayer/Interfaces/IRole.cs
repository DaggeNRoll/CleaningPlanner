using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRole
    {
        public IEnumerable<Role> GetAllRoles();
        public Role GetRole(int id);
        public Role GetRole(User user, Room room);
        public IEnumerable<Role> GetRolesWithUsers(Room room);
        public IEnumerable<Role> GetRolesByUser(int userId);
    
        public Role UpdateRole(Role role);
        public int DeleteRole(int id);
        public int DeleteRole(Role role);
        public void SaveRole(Role role);

    }
}
