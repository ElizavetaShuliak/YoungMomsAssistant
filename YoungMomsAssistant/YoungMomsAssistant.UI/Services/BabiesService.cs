using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Exceptions;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public class BabiesService : IBabiesService {

        IRequestJwtTokensDecorator _requestJwtTokensDecorator;

        public BabiesService(IRequestJwtTokensDecorator requestJwtTokensDecorator) {
            _requestJwtTokensDecorator = requestJwtTokensDecorator;
        }

        public async Task<List<Baby>> GetAllAsync() {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/";
            var result = await _requestJwtTokensDecorator.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.OK) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Baby>>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task AddAsync(Baby baby) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Add";
            var result = await _requestJwtTokensDecorator.PostAsync(url, baby);

            if (result.StatusCode != HttpStatusCode.OK) {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
        }

        public async Task UpdateAsync(Baby baby) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Update";
            var result = await _requestJwtTokensDecorator.PutAsync(url, baby);

            if (result.StatusCode != HttpStatusCode.OK) {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
        }

        public async Task DeleteAsync(int id) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Delete/{id}";
            var result = await _requestJwtTokensDecorator.DeleteAsync(url);

            if (result.StatusCode != HttpStatusCode.OK) {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
        }
    }
}
