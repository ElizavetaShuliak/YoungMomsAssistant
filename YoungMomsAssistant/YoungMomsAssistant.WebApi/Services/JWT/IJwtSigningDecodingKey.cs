using Microsoft.IdentityModel.Tokens;

namespace YoungMomsAssistant.WebApi.Services.JWT {
    public interface IJwtSigningDecodingKey {
        SecurityKey Key { get; }
    }
}
