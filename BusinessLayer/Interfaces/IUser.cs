using DataLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IUser
    {
        public IEnumerable<User> GetAllUsers();
        public User CreateUser(User user, LoginInformation loginInformation);
        public int DeleteUser(User user);
        public int DeleteUser(int id);
        public User GetUser(int id);
        public User GetUserByNickname(string nickname);
        public User GetUserByEmail(string email);
        public User UpdateUser(User user);
        public void SaveUser(User user);
        public void AddCleaningSpace(User user, CleaningSpace cleaningSpace);
    }
}
