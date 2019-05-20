using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DtoModels;

namespace YoungMomsAssistant.Core.Domain.Babies {
    public interface IBabyManager {
        Task<BabyGrowthDto> AddBabyGrowthsAsync(BabyGrowthDto babyGrowthDto, ClaimsPrincipal claimsPrincipal);
        Task<BabyWeightDto> AddBabyWeightsAsync(BabyWeightDto babyWeightDto, ClaimsPrincipal claimsPrincipal);
        Task<BabyDto> AddNewBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal);
        Task DeleteBabyAsync(int babyId, ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<BabyDto>> GetBabiesByUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<BabyGrowthDto>> GetBabyGrowthsAsync(int babyId, ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<BabyWeightDto>> GetBabyWeigthsAsync(int babyId, ClaimsPrincipal claimsPrincipal);
        Task UpdateBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal);
    }
}