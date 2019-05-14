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
    public class LifeEventsService : ILifeEventsService {
        IRequestJwtTokensDecorator _requestJwtTokensDecorator;

        public LifeEventsService(IRequestJwtTokensDecorator requestJwtTokensDecorator) {
            _requestJwtTokensDecorator = requestJwtTokensDecorator;
        }

        public async Task<List<LifeEvent>> GetAllAsync() {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/LifeEvents/";
            var result = await _requestJwtTokensDecorator.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.OK) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LifeEvent>>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task AddAsync(LifeEvent lifeEvent) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/LifeEvents/Add";
            var result = await _requestJwtTokensDecorator.PostAsync(url, lifeEvent);

            if (result.StatusCode != HttpStatusCode.OK) {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
        }

        public async Task UpdateAsync(LifeEvent lifeEvent) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/LifeEvents/Update";
            var result = await _requestJwtTokensDecorator.PutAsync(url, lifeEvent);

            if (result.StatusCode != HttpStatusCode.OK) {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
        }
    }
}
