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
        public IEnumerable<Role> GetAllUserRoles(User user);
        public Role GetRoleByUserAndRoom(User user, Room room);
        public Role CreateRole(User user, Room room);
        public Role UpdateRole(Role role);
        public int DeleteRole(Role role);
    }
}
