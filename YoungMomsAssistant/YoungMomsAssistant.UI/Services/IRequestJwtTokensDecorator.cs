using System.Net.Http;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Services {
    public interface IRequestJwtTokensDecorator {
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync<TBody>(string url, TBody body);
        Task<HttpResponseMessage> PutAsync<TBody>(string url, TBody body);
    }
}