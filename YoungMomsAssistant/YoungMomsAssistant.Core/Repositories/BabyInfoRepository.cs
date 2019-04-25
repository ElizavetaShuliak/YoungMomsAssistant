using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class BabyInfoRepository : IRepository<BabyInfo> {

        private AppDbContext _dbContext;
        private DbSet<BabyInfo> _babyInfos;

        public BabyInfoRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _babyInfos = _dbContext.BabyInfos;
        }

        public async Task AddAsync(BabyInfo model) {
            _babyInfos.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _babyInfos.FindAsync(id);

            _babyInfos.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BabyInfo> GetAsync(int id) => await _babyInfos.FindAsync(id);

        public async Task<List<BabyInfo>> GetAsync() => await _babyInfos.ToListAsync();

        public async Task UpdateAsync(BabyInfo model) {
            _babyInfos.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<BabyInfo> FindAsync(Expression<Func<BabyInfo, bool>> predicate)
            => _babyInfos.FirstOrDefaultAsync(predicate);
    }
}
