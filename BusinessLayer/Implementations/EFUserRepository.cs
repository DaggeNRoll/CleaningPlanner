using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class EFUserRepository : IUser
    {
        private DSDbContext _context;

        public EFUserRepository(DSDbContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public int DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAlUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
