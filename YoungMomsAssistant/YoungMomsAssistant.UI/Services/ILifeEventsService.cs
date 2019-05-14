using System.Collections.Generic;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Models;

namespace YoungMomsAssistant.UI.Services {
    public interface ILifeEventsService {
        Task AddAsync(LifeEvent lifeEvent);
        Task<List<LifeEvent>> GetAllAsync();
        Task UpdateAsync(LifeEvent lifeEvent);
    }
}