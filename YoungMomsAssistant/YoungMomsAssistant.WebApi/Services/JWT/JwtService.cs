using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.WebApi.Constants;

namespace YoungMomsAssistant.WebApi.Services.JWT {
    public class JwtService : IJwtService {

        private IJwtSigningEncodingKey _jwtSigningEncodingKey;
        private TokenValidationParameters _tokenValidationParameters;

        public JwtService(
            IJwtSigningEncodingKey jwtSigningEncodingKey,
            TokenValidationParameters tokenValidationParameters) {
            _jwtSigningEncodingKey = jwtSigningEncodingKey;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = _tokenValidationParameters.ValidateIssuerSigningKey,
                IssuerSigningKey = _tokenValidationParameters.IssuerSigningKey,

                ValidateIssuer = _tokenValidationParameters.ValidateIssuer,
                ValidIssuer = _tokenValidationParameters.ValidIssuer,

                ValidateAudience = _tokenValidationParameters.ValidateAudience,
                ValidAudience = _tokenValidationParameters.ValidAudience,

                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || jwtSecurityToken.Header.Alg != _jwtSigningEncodingKey.SigningAlgorithm)
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public string GenerateToken(UserDto userDto) {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var token = new JwtSecurityToken(
                issuer: AuthConstants.Issuer,
                audience: AuthConstants.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(AuthConstants.LifeTimeMins),
                signingCredentials: new SigningCredentials(
                    _jwtSigningEncodingKey.Key,
                    _jwtSigningEncodingKey.SigningAlgorithm)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
