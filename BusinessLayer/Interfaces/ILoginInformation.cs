using DataLayer.Entities;
using System.Collections.Generic;

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
        public void SaveLoginInformation(LoginInformation loginInformation);
    }
}
