using Microsoft.IdentityModel.Tokens;

namespace YoungMomsAssistant.Core.Domain.Users.JWT {
    public interface IJwtSigningDecodingKey {
        SecurityKey Key { get; }
    }
}
