using System.Collections.Generic;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public interface IBabiesService {
        Task<Baby> AddAsync(Baby baby);
        Task<BabyGrowth> AddGrowthAsync(BabyGrowth baby);
        Task<BabyWeight> AddWeightAsync(BabyWeight baby);
        Task DeleteAsync(int id);
        Task<List<Baby>> GetAllAsync();
        Task<List<BabyGrowth>> GetGrowthsAsync(int id);
        Task<List<BabyWeight>> GetWeightsAsync(int id);
        Task UpdateAsync(Baby baby);
    }
}