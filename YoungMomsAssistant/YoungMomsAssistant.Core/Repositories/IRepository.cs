using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace YoungMomsAssistant.Core.Repositories {
    public interface IRepository<TModel> {
        Task<TModel> GetAsync(int id);

        Task<List<TModel>> GetAsync();

        Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate);

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task RemoveAsync(int id);
    }
}
