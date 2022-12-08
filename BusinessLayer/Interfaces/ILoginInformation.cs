using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILoginInformation
    {
        public IEnumerable<LoginInformation> GetAllLoginInformations();
        public LoginInformation GetLoginInformation(User user);
        public LoginInformation GetLoginInformation(int id);
        public LoginInformation UpdateLoginInformation(LoginInformation loginInformation);
        public int DeleteLoginInformation(LoginInformation loginInformation);
        public int DeleteLoginInformation(int id);
        public int DeleteLoginInformation(User user);
    }
}
