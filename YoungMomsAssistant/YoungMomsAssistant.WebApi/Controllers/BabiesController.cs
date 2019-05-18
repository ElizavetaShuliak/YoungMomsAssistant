using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoungMomsAssistant.Core.Domain.Babies;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.WebApi.Services.JWT;

namespace YoungMomsAssistant.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BabiesController : ControllerBase {

        private IBabyManager _babyManager;

        public BabiesController(IBabyManager babyManager) {
            _babyManager = babyManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get() {
            var babies = await _babyManager.GetBabiesByUserAsync(HttpContext.User);

            return Ok(babies);
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] BabyDto babyDto) {
             await _babyManager.AddNewBabyAsync(babyDto, HttpContext.User);

            return Ok();
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] BabyDto babyDto) {
            await _babyManager.UpdateBabyAsync(babyDto, HttpContext.User);

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id) {
            await _babyManager.DeleteBabyAsync(id, HttpContext.User);

            return Ok();
        }
    }
}