using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace YoungMomsAssistant.WebApi.Services.JWT {
    public class SigningSymmetricKey : IJwtSigningDecodingKey, IJwtSigningEncodingKey {

        public SigningSymmetricKey(string key) {
            SigningAlgorithm = SecurityAlgorithms.HmacSha256;
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public SecurityKey Key { get; }

        public string SigningAlgorithm { get; }
    }
}
