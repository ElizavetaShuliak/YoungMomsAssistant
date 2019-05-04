using System.Threading.Tasks;
using YoungMomsAssistant.Core.Domain.Users.Utilities;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.Core.Repositories;

namespace YoungMomsAssistant.Core.Domain.Users {
    public class UserManager : IUserManager {

        private IRepository<User> _userRepo;

        public UserManager(IRepository<User> userRepository) {
            _userRepo = userRepository;
        }

        public async Task<UserDto> AuthUserAsync(UserDto userDto) {
            var userToLogin = await _userRepo
                .FindAsync(user => user.Email == userDto.Email || user.Login == userDto.Login);

            return userToLogin?.PasswordHash != UserUtility.GetPasswordHash(userDto.Password)
                ? null : new UserDto {
                    Login = userToLogin.Login,
                    Email = userToLogin.Email,
                    Password = userDto.Password
                };
        }

        public async Task<bool> RegisterAsync(UserDto userDto) {
            var oldUser = await _userRepo
                .FindAsync(user => user.Email == userDto.Email || user.Login == userDto.Login);

            if (oldUser != null) {
                return false;
            }

            await _userRepo.AddAsync(new User {
                Login = userDto.Login,
                Email = userDto.Email,
                PasswordHash = UserUtility.GetPasswordHash(userDto.Password)
            });

            return true;
        }
    }
}
