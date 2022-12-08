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
        public IEnumerable<LoginInformation> GetAllLoginInformation();
        public LoginInformation GetLoginInformationByUser(User user);
        public LoginInformation AddLoginInformation(LoginInformation loginInformation);
        public LoginInformation UpdateLoginInformation(LoginInformation loginInformation);
        public int DeleteLoginInformation(LoginInformation loginInformation);
    }
}
