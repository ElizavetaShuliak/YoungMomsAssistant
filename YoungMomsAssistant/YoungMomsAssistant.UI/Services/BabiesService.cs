using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
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

        public async Task<Baby> AddAsync(Baby baby) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Add";
            var result = await _requestJwtTokensDecorator.PostAsync(url, baby);

            if (result.StatusCode == HttpStatusCode.Created) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Baby>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotCreatedResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task UpdateAsync(Baby baby) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Update";
            var result = await _requestJwtTokensDecorator.PutAsync(url, baby);


            if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else if (result.StatusCode != HttpStatusCode.OK) {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task DeleteAsync(int id) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Delete/{id}";
            var result = await _requestJwtTokensDecorator.DeleteAsync(url);


            if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else if (result.StatusCode != HttpStatusCode.NoContent) {
                throw new NotNoContentResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task<List<BabyWeight>> GetWeightsAsync() {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Weights";
            var result = await _requestJwtTokensDecorator.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.OK) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BabyWeight>>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task<BabyWeight> AddWeightAsync(BabyWeight weight) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Weights/Add";
            var result = await _requestJwtTokensDecorator.PostAsync(url, weight);

            if (result.StatusCode == HttpStatusCode.Created) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BabyWeight>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotCreatedResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task<List<BabyGrowth>> GetGrowthsAsync() {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Growths";
            var result = await _requestJwtTokensDecorator.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.OK) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BabyGrowth>>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotOkResponseException(((int)result.StatusCode).ToString());
            }
        }

        public async Task<BabyGrowth> AddGrowthAsync(BabyGrowth growth) {
            var url = $@"{ConfigurationSettings.AppSettings["WebApiUrl"]}/Babies/Growths/Add";
            var result = await _requestJwtTokensDecorator.PostAsync(url, growth);

            if (result.StatusCode == HttpStatusCode.Created) {
                var jsonContentString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BabyGrowth>(jsonContentString);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new AuthorizationException();
            }
            else {
                throw new NotCreatedResponseException(((int)result.StatusCode).ToString());
            }
        }
    }
}
