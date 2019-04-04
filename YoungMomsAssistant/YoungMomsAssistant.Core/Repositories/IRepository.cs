using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoungMomsAssistant.Core.Repositories {
    public interface IRepository<TModel> {
        Task<TModel> GetAsync(int id);

        Task<List<TModel>> GetAsync();

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task RemoveAsync(int id);
    }
}
