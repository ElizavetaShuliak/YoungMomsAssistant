using System.Collections.Generic;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public interface IBabiesService {
        Task AddAsync(Baby baby);
        Task DeleteAsync(int id);
        Task<List<Baby>> GetAllAsync();
        Task UpdateAsync(Baby baby);
    }
}