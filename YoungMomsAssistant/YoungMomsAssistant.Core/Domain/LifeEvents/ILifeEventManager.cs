using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DtoModels;

namespace YoungMomsAssistant.Core.Domain.LifeEvents {
    public interface ILifeEventManager {
        Task AddNewLifeEventAsync(LifeEventDto lifeEventDto, ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<LifeEventDto>> GetLifeEventsByUserAsync(ClaimsPrincipal claimsPrincipal);
        Task UpdateLifeEventAsync(LifeEventDto lifeEventDto, ClaimsPrincipal claimsPrincipal);
    }
}