﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DtoModels;

namespace YoungMomsAssistant.Core.Domain.LifeEvents {
    public interface ILifeEventManager {
        Task<LifeEventDto> AddNewLifeEventAsync(LifeEventDto lifeEventDto, ClaimsPrincipal claimsPrincipal);
        Task DeleteLifeEventAsync(int babyId, ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<LifeEventDto>> GetLifeEventsByDateAsync(ClaimsPrincipal claimsPrincipal, System.DateTime date);
        Task<IEnumerable<LifeEventDto>> GetLifeEventsByUserAsync(ClaimsPrincipal claimsPrincipal);
        Task UpdateLifeEventAsync(LifeEventDto lifeEventDto, ClaimsPrincipal claimsPrincipal);
    }
}