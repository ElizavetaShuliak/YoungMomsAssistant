namespace YoungMomsAssistant.WebApi.Constants {
    static class AuthConstants {
        public const string SigningSecurityKey = "e58794d0a8c6314b94398d47f391e5db09499ec5";
        public const string Audience = "YoungMomsAssistant.App";
        public const string Issuer = "YoungMomsAssistant.WebApi";
        public const string JwtSchemeName = "JwtBearer";
        public const int LifeTimeMins = 1;
        public const int ClockSkew = 5;
    }
}
