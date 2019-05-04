using System.Threading.Tasks;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public interface IAuthorizationTokensService {
        JwtTokens Tokens { get; set; }

        Task<JwtTokens> RefreshTokenAsync();
    }
}