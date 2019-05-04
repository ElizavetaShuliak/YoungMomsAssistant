using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Infrastructure.Extensions {
    public static class HttpClientExtansions {
        public static async Task<HttpResponseMessage> PostWithJsonBodyAsync<T>(this HttpClient httpClient, string url, T body) {
            var jsonRequestString = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url, content);
        }
    }
}
