using Microsoft.IdentityModel.Tokens;

namespace YoungMomsAssistant.WebApi.Services.JWT {
    public interface IJwtSigningEncodingKey {
        string SigningAlgorithm { get; }

        SecurityKey Key { get; }
    }
}
