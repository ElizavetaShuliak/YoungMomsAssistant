using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    class AuthenticationService {
        public async Task<string> SignInAsync(User user) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Authentication/SignIn";

            var jsonContentString = $"{{Login:'{user.Login}',Email:'{user.Email}',Password:'{user.Password}'}}";

            var content = new StringContent(jsonContentString, Encoding.UTF8, "application/json");

            using (var request = new HttpClient()) {
                var result = await request.PostAsync(url, content);
                return await result.Content.ReadAsStringAsync();
                // TODO: errors handling
            }
        }

        public async Task SignOutAsync(User user) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Authentication/SignOut";

            var jsonContentString = $"{{Login:'{user.Login}',Email:'{user.Email}',Password:'{user.Password}'}}";

            var content = new StringContent(jsonContentString, Encoding.UTF8, "application/json");

            using (var request = new HttpClient()) {
                var result = await request.PostAsync(url, content);
                // TODO: errors handling
            }
        }
    }
}
