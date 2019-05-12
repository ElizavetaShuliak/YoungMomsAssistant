using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Services {
    public class RequestJwtTokensDecorator : IRequestJwtTokensDecorator {

        AuthorizationTokensService _authorizationTokensService;

        public RequestJwtTokensDecorator(AuthorizationTokensService authorizationTokensService) {
            _authorizationTokensService = authorizationTokensService;
        }

        public async Task<HttpResponseMessage> PostAsync<TBody>(string url, TBody body) {
            using (var httpClient = new HttpClient()) {
                var jsonRequestString = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                AddAuthorizationHeader(httpClient);

                var result = await httpClient.PostAsync(url, content);

                if (result.StatusCode != System.Net.HttpStatusCode.Unauthorized) {
                    return result;
                }
                else {
                    await _authorizationTokensService.RefreshTokenAsync();
                    AddAuthorizationHeader(httpClient);
                    return await httpClient.PostAsync(url, content);
                }
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string url) {
            using (var httpClient = new HttpClient()) {
                AddAuthorizationHeader(httpClient);

                var result = await httpClient.GetAsync(url);

                if (result.StatusCode != System.Net.HttpStatusCode.Unauthorized) {
                    return result;
                }
                else {
                    await _authorizationTokensService.RefreshTokenAsync();
                    AddAuthorizationHeader(httpClient);
                    return await httpClient.GetAsync(url);
                }
            }
        }

        private void AddAuthorizationHeader(HttpClient httpClient)
            => httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _authorizationTokensService.Tokens.Token);
    }
}
