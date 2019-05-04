using System.Security.Claims;

namespace YoungMomsAssistant.WebApi.Services.JWT {
    public interface IJwtService {
        string GenerateToken(Core.Models.DtoModels.UserDto userDto);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}