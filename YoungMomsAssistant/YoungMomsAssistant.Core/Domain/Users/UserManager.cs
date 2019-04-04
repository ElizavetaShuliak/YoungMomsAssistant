using System.Threading.Tasks;
using YoungMomsAssistant.Core.Domain.Users.Utilities;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.Core.Repositories;

namespace YoungMomsAssistant.Core.Domain.Users {
    public class UserManager {

        private IRepository<User> _userRepo;

        public UserManager(IRepository<User> userRepository) {
            _userRepo = userRepository;
        }

        public async Task<User> LoginAsync(string loginOrEmail, string password) {
            var userToLogin = await _userRepo
                .FindAsync(user => user.Email == loginOrEmail || user.Login == loginOrEmail);

            return userToLogin?.PasswordHash == UserUtility.GetPasswordHash(password) 
                ? userToLogin : null;
        }

        public async Task<User> RegisterAsync(UserDto userDto) {
            var oldUser = await _userRepo
                .FindAsync(user => user.Email == userDto.Email || user.Login == userDto.Login);

            if (oldUser != null) {
                return null;
            }

            await _userRepo.AddAsync(new User {
                Login = userDto.Login,
                Email = userDto.Email,
                PasswordHash = UserUtility.GetPasswordHash(userDto.Password)
            });

            // temp
            return await _userRepo
                .FindAsync(user => user.Email == userDto.Email && user.Login == userDto.Login);
        }
    }
}
