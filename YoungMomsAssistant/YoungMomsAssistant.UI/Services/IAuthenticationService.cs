using System.Threading.Tasks;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public interface IAuthenticationService {
        Task<JwtTokens> SignInAsync(User user);
        Task<bool> SignUpAsync(User user);
    }
}