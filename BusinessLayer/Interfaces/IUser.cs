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
        public User CreateUser(User user, LoginInformation loginInformation);
        public int DeleteUser(User user);
        public int DeleteUser(int id);
        public User GetUser(int id);
        public User UpdateUser(User user);
        public void SaveUser(User user);
        public void AddCleaningSpace(User user, CleaningSpace cleaningSpace);
    }
}
