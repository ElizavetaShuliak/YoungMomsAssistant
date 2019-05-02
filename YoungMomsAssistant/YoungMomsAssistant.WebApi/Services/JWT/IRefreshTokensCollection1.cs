namespace YoungMomsAssistant.WebApi.Services.JWT {
    public interface IRefreshTokensCollection1 {
        void Add(string token, string key);
        string Generate(string key);
        string Get(string key);
    }
}