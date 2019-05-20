using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class BabyGrowthRepository : IRepository<BabyGrowth> {

        private AppDbContext _dbContext;
        private DbSet<BabyGrowth> _babyGrowths;

        public BabyGrowthRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _babyGrowths = _dbContext.BabyGrowths;
        }

        public async Task AddAsync(BabyGrowth model) {
            _babyGrowths.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _babyGrowths.FindAsync(id);

            _babyGrowths.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BabyGrowth> GetAsync(int id) => await _babyGrowths.FindAsync(id);

        public async Task<List<BabyGrowth>> GetAsync() => await _babyGrowths.ToListAsync();

        public async Task UpdateAsync(BabyGrowth model) {
            _babyGrowths.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<BabyGrowth> FindAsync(Expression<Func<BabyGrowth, bool>> predicate)
            => _babyGrowths.FirstOrDefaultAsync(predicate);

        public Task<List<BabyGrowth>> FindAllAsync(Expression<Func<BabyGrowth, bool>> predicate)
            => _babyGrowths.Where(predicate).ToListAsync();
    }
}
