using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class EFRoleRepository : IRole
    {
        private DSDbContext _context;

        public EFRoleRepository(DSDbContext context)
        {
            _context = context;
        }

        public int DeleteRole(int id)
        {
            var roleToDelete = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (roleToDelete == null)
            {
                return -1;
            }

            _context.Remove(roleToDelete);
            _context.SaveChanges();
            return 0;
        }

        public int DeleteRole(Role role)
        {
            var roleToDelete = _context.Roles.FirstOrDefault(r => r.Id == role.Id);

            if (roleToDelete == null)
            {
                return -1;
            }

            _context.Remove(roleToDelete);
            _context.SaveChanges();
            return 0;
        }

        public IEnumerable<Role> GetAllRoles()//надо проверить инклюды
        {
            return _context.Roles.Include(r => r.User).Include(r => r.User.Room).ToList();
        }

        public Role GetRole(int id)
        {
            return _context.Roles.Include(r => r.User).Include(r => r.Room).FirstOrDefault(r => r.Id == id);
        }

        public Role GetRole(User user, Room room)
        {
            return _context.Roles.FirstOrDefault(r => r.UserId == user.Id && r.RoomId == room.Id);
        }

        public Role GetRoleByUser(int userId)
        {
            return _context.Roles.Include(r => r.User).Include(r => r.Room).FirstOrDefault(r => r.UserId == userId);
        }

        public IEnumerable<Role> GetRolesWithUsers(Room room)
        {
            return _context.Roles.Where(r => r.RoomId == room.Id).Include(r => r.User).ToList();
        }

        public void SaveRole(Role role)
        {
            if (role.Id != 0)
            {
                _context.Entry(role).State = EntityState.Modified;
            }
            else
            {
                _context.Add(role);
            }
            _context.SaveChanges();
        }

        public Role UpdateRole(Role role)
        {
            var roleToBeUpdated = _context.Roles.FirstOrDefault(r => r.Id == role.Id);

            if (roleToBeUpdated == null)
            {
                return null;
            }

            roleToBeUpdated = role;
            _context.SaveChanges();

            return roleToBeUpdated;
        }
    }
}
