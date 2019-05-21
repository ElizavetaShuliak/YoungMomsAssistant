using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Domain.Babies;
using YoungMomsAssistant.Core.Models.DtoModels;

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
            var result = await _babyManager.AddNewBabyAsync(babyDto, HttpContext.User);

            return CreatedAtAction("Add", result);
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

            return NoContent();
        }

        [HttpGet("Weights/{id}")]
        [Authorize]
        public async Task<ActionResult> GetWeights(int id) {
            var weights = await _babyManager.GetBabyWeigthsAsync(id, HttpContext.User);

            return Ok(weights);
        }

        [HttpPost("Weights/Add")]
        [Authorize]
        public async Task<ActionResult> AddWeight([FromBody] BabyWeightDto babyWeightDto) {
            var weight = await _babyManager.AddBabyWeightsAsync(babyWeightDto, HttpContext.User);

            return CreatedAtAction("Add", weight);
        }


        [HttpGet("Growths/{id}")]
        [Authorize]
        public async Task<ActionResult> GetGrowths(int id) {
            var growths = await _babyManager.GetBabyGrowthsAsync(id, HttpContext.User);

            return Ok(growths);
        }

        [HttpPost("Growths/Add")]
        [Authorize]
        public async Task<ActionResult> AddGrowth([FromBody] BabyGrowthDto babyGrowthDto) {
            var growth = await _babyManager.AddBabyGrowthsAsync(babyGrowthDto, HttpContext.User);

            return CreatedAtAction("Add", growth);
        }
    }
}