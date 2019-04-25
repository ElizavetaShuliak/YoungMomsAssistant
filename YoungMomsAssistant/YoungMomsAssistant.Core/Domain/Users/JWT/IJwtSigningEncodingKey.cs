using Microsoft.IdentityModel.Tokens;

namespace YoungMomsAssistant.Core.Domain.Users.JWT {
    public interface IJwtSigningEncodingKey {
        string SigningAlgorithm { get; }

        SecurityKey Key { get; }
    }
}
