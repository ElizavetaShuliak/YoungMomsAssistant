using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DtoModels;

namespace YoungMomsAssistant.Core.Domain.Babies {
    public interface IBabyManager {
        Task AddNewBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<BabyDto>> GetBabiesByUserAsync(ClaimsPrincipal claimsPrincipal);
    }
}