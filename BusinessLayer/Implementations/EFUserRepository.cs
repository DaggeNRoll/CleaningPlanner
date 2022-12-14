using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class EFUserRepository : IUser
    {
        private DSDbContext _context;
        private EFCleaningSpaceRepository _cleaningSpaces;

        public EFUserRepository(DSDbContext context)
        {
            _context = context;
        }

        public User CreateUser(User user, LoginInformation loginInformation)
        {
            //loginInformation.User = user;
            user.LoginInformation = loginInformation;
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public int DeleteUser(User user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToDelete != null)
            {
                _context.Remove(userToDelete);
                _context.SaveChanges();
                return 0;
            }
            return -1;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Role).Include(u => u.Room).Include(u => u.CleaningSpaces).ToList();
        }

        public User GetUser(int id)
        {
            var user = _context.Users.Include(u => u.Room).Include(u => u.Role).Include(u => u.CleaningSpaces).Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public User UpdateUser(User user)
        {
            var userToUpdate = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate = user;
                _context.SaveChanges();
                return userToUpdate;
            }

            return null;
        }

        public void SaveUser(User user)
        {
            if (user.Id != 0)
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            else
            {
                _context.Add(user);
            }
            _context.SaveChanges();
        }

        public void AddCleaningSpace(User user, CleaningSpace cleaningSpace)
        {
            if (user.CleaningSpaces.Contains(cleaningSpace))
                return;

            user.CleaningSpaces.Add(cleaningSpace);
            _context.SaveChanges();
        }

        public User GetUserByNickname(string nickname)
        {
            var user = _context.Users.Include(u => u.Room).Include(u => u.CleaningSpaces).Include(u => u.Role).FirstOrDefault(u => u.NickName == nickname);
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.Include(u => u.Room).Include(u => u.CleaningSpaces).Include(u => u.Role).FirstOrDefault(u => u.Email == email);
            return user;
        }
    }
}
