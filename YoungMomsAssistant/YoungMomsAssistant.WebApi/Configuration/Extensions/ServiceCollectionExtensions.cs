using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using YoungMomsAssistant.WebApi.Services.JWT;
using YoungMomsAssistant.WebApi.Constants;

namespace YoungMomsAssistant.WebApi.Configuration.Extensions {
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddCustomJwtAuth(this IServiceCollection services) {
            var signingKey = new SigningSymmetricKey(AuthConstants.SigningSecurityKey);

            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            services.AddSingleton<IJwtSigningDecodingKey>(signingKey);

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingDecodingKey.Key,

                ValidateIssuer = true,
                ValidIssuer = AuthConstants.Issuer,

                ValidateAudience = true,
                ValidAudience = AuthConstants.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromSeconds(AuthConstants.ClockSkew)
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IRefreshTokensCollection, RefreshTokensCollection>();

            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = AuthConstants.JwtSchemeName;
                    options.DefaultChallengeScheme = AuthConstants.JwtSchemeName;
                })
                .AddJwtBearer(AuthConstants.JwtSchemeName, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = tokenValidationParameters;
                });

            return services;
        } 
    }
}
