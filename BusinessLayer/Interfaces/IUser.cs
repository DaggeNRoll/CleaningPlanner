using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUser
    {
        public IEnumerable<User> GetAlUsers();
        public User GetUser(int id);
        public User CreateUser(User user);
        public User UpdateUser(User user);
        public int DeleteUser(User user);
    }
}
