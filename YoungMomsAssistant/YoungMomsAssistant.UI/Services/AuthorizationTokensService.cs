using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Infrastructure.Extensions;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public class AuthorizationTokensService : IAuthorizationTokensService {

        public JwtTokens Tokens { get; set; }

        public async Task<JwtTokens> RefreshTokenAsync() {
            if (Tokens == null) {
                throw new NullReferenceException("Tokens");
            }

            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Authentication/RefreshToken";

            using (var request = new HttpClient()) {
                var result = await request.PostWithJsonBodyAsync(url, Tokens);

                if (result.StatusCode == HttpStatusCode.OK) {
                    var jsonContentString = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JwtTokens>(jsonContentString);
                }
                else {
                    throw new NotOkResponseException(((int)result.StatusCode).ToString());
                }
            }
        }
    }
}
