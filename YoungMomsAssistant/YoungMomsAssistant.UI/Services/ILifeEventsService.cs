using System.Collections.Generic;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public interface ILifeEventsService {
        Task<LifeEvent> AddAsync(LifeEvent lifeEvent);
        Task DeleteAsync(int id);
        Task<List<LifeEvent>> GetAllAsync();
        Task<List<LifeEvent>> GetByDateAsync(System.DateTime date);
        Task UpdateAsync(LifeEvent lifeEvent);
    }
}