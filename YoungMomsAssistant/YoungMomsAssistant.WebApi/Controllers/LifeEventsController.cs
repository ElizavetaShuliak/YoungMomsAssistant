﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoungMomsAssistant.Core.Domain.LifeEvents;
using YoungMomsAssistant.Core.Models.DtoModels;

namespace YoungMomsAssistant.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LifeEventsController : ControllerBase {

        private ILifeEventManager _lifeEventManager;

        public LifeEventsController(ILifeEventManager lifeEventManager) {
            _lifeEventManager = lifeEventManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get() {
            var babies = await _lifeEventManager.GetLifeEventsByUserAsync(HttpContext.User);

            return Ok(babies);
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] LifeEventDto lifeEventDto) {
            await _lifeEventManager.AddNewLifeEventAsync(lifeEventDto, HttpContext.User);

            return Ok();
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] LifeEventDto lifeEventDto) {
            await _lifeEventManager.UpdateLifeEventAsync(lifeEventDto, HttpContext.User);

            return Ok();
        }
    }
}