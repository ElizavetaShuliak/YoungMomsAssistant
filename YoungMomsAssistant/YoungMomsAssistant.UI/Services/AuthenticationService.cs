using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Infrastructure.Extensions;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    class AuthenticationService : IAuthenticationService {
        public async Task<JwtTokens> SignInAsync(User user) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Authentication/SignIn";

            using (var request = new HttpClient()) {
                var result = await request.PostWithJsonBodyAsync(url, user);

                if (result.StatusCode == HttpStatusCode.OK) {
                    var jsonContentString = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JwtTokens>(jsonContentString);
                }
                else {
                    throw new NotOkResponseException(((int)result.StatusCode).ToString());
                }
            }
        }

        public async Task<bool> SignUpAsync(User user) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Authentication/SignUp";

            using (var request = new HttpClient()) {
                var result = await request.PostWithJsonBodyAsync(url, user);

                if (result.StatusCode == HttpStatusCode.OK) {
                    return true;
                }
                else {
                    throw new NotOkResponseException(((int)result.StatusCode).ToString());
                }
            }
        }
    }
}
