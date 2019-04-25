using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;

namespace YoungMomsAssistant.Core.Domain.Users {
    public interface IUserManager {
        Task<UserDto> AuthUserAsync(UserDto userDto);
        Task<bool> RegisterAsync(UserDto userDto);
    }
}