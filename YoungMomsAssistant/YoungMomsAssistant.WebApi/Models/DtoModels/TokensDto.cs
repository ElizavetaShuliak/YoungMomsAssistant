using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungMomsAssistant.WebApi.Models.DtoModels {
    public class TokensDto {

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
