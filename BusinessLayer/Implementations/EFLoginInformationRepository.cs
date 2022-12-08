using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;



namespace BusinessLayer.Implementations
{
    public class EFLoginInformationRepository : ILoginInformation
    {
        private DSDbContext _context;
        public EFLoginInformationRepository(DSDbContext context)
        {
           _context = context;
        }
        public int DeleteLoginInformation(LoginInformation loginInformation)
        {
            var loginToDelete = _context.LoginInformations.FirstOrDefault(l=>l==loginInformation);
            if (loginToDelete != null)
            {
                _context.Remove(loginToDelete);
                _context.SaveChanges();
                return 0;
            }
            return -1;
        }

        public int DeleteLoginInformation(int id)
        {
            var loginToDelete=_context.LoginInformations.FirstOrDefault(l=>l.Id==id);
            if (loginToDelete != null)
            {
                _context.Remove(loginToDelete);
                _context.SaveChanges();
                return 0;
            }
            return -1;
        }

        public int DeleteLoginInformation(User user)
        {
            var loginToDelete = _context.LoginInformations.Include(u=>u.User).FirstOrDefault(l => l.User == user);
            if (loginToDelete != null)
            {
                _context.Remove(loginToDelete);
                _context.SaveChanges();
                return 0;
            }
            return -1;
        }

        public IEnumerable<LoginInformation> GetAllLoginInformations()
        {
            return _context.LoginInformations.ToList();
        }

        public LoginInformation GetLoginInformation(User user)
        {
            return _context.LoginInformations.FirstOrDefault(l => l.User == user);
        }

        public LoginInformation GetLoginInformation(int id)
        {
            return _context.LoginInformations.FirstOrDefault(l => l.Id == id);
        }

        public LoginInformation UpdateLoginInformation(LoginInformation loginInformation)
        {
            var loginToUpdate = _context.LoginInformations.FirstOrDefault(l => l == loginInformation);
            if (loginToUpdate != null)
            {
                loginToUpdate = loginInformation;
                _context.SaveChanges();
                return loginToUpdate;
            }
            return null;
        }
    }
}
