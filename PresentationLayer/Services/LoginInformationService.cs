using BusinessLayer;
using DataLayer.Entities;
using PresentationLayer.Models;

namespace PresentationLayer.Services
{
    public class LoginInformationService
    {
        private DataManager _dataManager;
        //private UserService _userService;

        public LoginInformationService(DataManager dataManager)
        {
            _dataManager = dataManager;
            //_userService = new UserService(dataManager);
        }

        public LoginInformationViewModel LoginInformationDbToViewModel(int id)
        {
            var loginFromDb = _dataManager.LoginInformationRepository.GetLoginInformation(id);

            return new LoginInformationViewModel()
            {
                LoginInformation = loginFromDb,
                //User = _userService.UserDbModelToView(loginFromDb.UserId)
            };
        }

        public LoginInformationEditModel GetLoginInformationEditModel(int id)//hash epta
        {
            var loginFromDb = _dataManager.LoginInformationRepository.GetLoginInformation(id);

            return new LoginInformationEditModel()
            {
                Id = loginFromDb.Id,
                Login = loginFromDb.Login,
                Password = loginFromDb.Password,
                UserId = loginFromDb.UserId,
            };
        }

        public LoginInformationViewModel SaveLoginInformationEditModelToDb(LoginInformationEditModel loginEditModel)
        {
            LoginInformation login;

            if (loginEditModel.Id != 0)
            {
                login = _dataManager.LoginInformationRepository.GetLoginInformation(loginEditModel.Id);
            }
            else
            {
                login = new LoginInformation()
                {
                    Id = loginEditModel.Id,
                    Login = loginEditModel.Login,
                    Password = loginEditModel.Password,
                    UserId = loginEditModel.UserId,
                };
            }
            _dataManager.LoginInformationRepository.SaveLoginInformation(login);

            return LoginInformationDbToViewModel(login.Id);
        }
    }
}
