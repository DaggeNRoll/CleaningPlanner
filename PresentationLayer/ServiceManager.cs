using BusinessLayer;
using PresentationLayer.Services;

namespace PresentationLayer
{
    public class ServiceManager
    {
        private DataManager _dataManager;
        private UserService _userService;
        private RoomService _roomService;
        private LoginInformationService _loginInformationService;
        private RoleService _roleService;
        private CleaningSpaceService _cleaningSpaceservice;

        public ServiceManager(DataManager dataManager)
        {
            _dataManager = dataManager;
            _userService = new UserService(dataManager);
            _roomService = new RoomService(dataManager);
            _loginInformationService = new LoginInformationService(dataManager);
            _roleService = new RoleService(dataManager);
            _cleaningSpaceservice = new CleaningSpaceService(dataManager);
        }

        public DataManager DataManager { get => _dataManager; }
        public UserService UserService { get => _userService; }
        public RoomService RoomService { get => _roomService; }
        public LoginInformationService LoginInformationService { get => _loginInformationService; }
        public RoleService RoleService { get => _roleService; }
        public CleaningSpaceService CleaningSpaceService { get => _cleaningSpaceservice; }
    }
}
