using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    public class DataManager
    {
        private ICleaningSpace _cleaningSpaceReposioty;
        private ILoginInformation _loginInformationRepository;
        private IRole _roleRepository;
        private IRoom _roomRepository;
        private IUser _userRepository;

        public DataManager(ICleaningSpace cleaningSpaceReposioty, ILoginInformation loginInformationRepository, IRole roleRepository, IRoom roomRepository, IUser userRepository)
        {
            _cleaningSpaceReposioty = cleaningSpaceReposioty;
            _loginInformationRepository = loginInformationRepository;
            _roleRepository = roleRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public ICleaningSpace CleaningSpaceRepository => _cleaningSpaceReposioty;
        public ILoginInformation LoginInformationRepository => _loginInformationRepository;
        public IRole RoleRepository => _roleRepository;
        public IRoom RoomRepository => _roomRepository;
        public IUser UserRepository => _userRepository;
    }
}
