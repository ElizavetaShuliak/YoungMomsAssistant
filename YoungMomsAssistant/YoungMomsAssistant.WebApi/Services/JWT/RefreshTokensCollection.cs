using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace YoungMomsAssistant.WebApi.Services.JWT {
    public class RefreshTokensCollection : IRefreshTokensCollection {
        private Dictionary<string, string> _refreshTokens;

        public RefreshTokensCollection() {
            _refreshTokens = new Dictionary<string, string>();
        }

        public void Add(string token, string key) {
            _refreshTokens[key] = token;
        }

        public string Get(string key) {
            return _refreshTokens[key];
        }

        public string Generate(string key) {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                var token = Convert.ToBase64String(randomNumber);

                _refreshTokens[key] = token;
                return token;
            }

        }
    }
}
