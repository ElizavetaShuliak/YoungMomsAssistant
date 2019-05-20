using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class BabyWeightRepository : IRepository<BabyWeight> {

        private AppDbContext _dbContext;
        private DbSet<BabyWeight> _babyWeights;

        public BabyWeightRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _babyWeights = _dbContext.BabyWeights;
        }

        public async Task AddAsync(BabyWeight model) {
            _babyWeights.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _babyWeights.FindAsync(id);

            _babyWeights.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BabyWeight> GetAsync(int id) => await _babyWeights.FindAsync(id);

        public async Task<List<BabyWeight>> GetAsync() => await _babyWeights.ToListAsync();

        public async Task UpdateAsync(BabyWeight model) {
            _babyWeights.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<BabyWeight> FindAsync(Expression<Func<BabyWeight, bool>> predicate)
            => _babyWeights.FirstOrDefaultAsync(predicate);

        public Task<List<BabyWeight>> FindAllAsync(Expression<Func<BabyWeight, bool>> predicate)
            => _babyWeights.Where(predicate).ToListAsync();
    }
}
