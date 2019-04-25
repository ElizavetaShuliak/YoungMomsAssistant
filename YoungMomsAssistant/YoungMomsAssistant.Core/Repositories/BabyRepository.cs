using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class BabyRepository : IRepository<Baby> {

        private AppDbContext _dbContext;
        private DbSet<Baby> _babies;

        public BabyRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _babies = _dbContext.Babies;
        }

        public async Task AddAsync(Baby model) {
            _babies.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _babies.FindAsync(id);

            _babies.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Baby> GetAsync(int id) => await _babies.FindAsync(id);

        public async Task<List<Baby>> GetAsync() => await _babies.ToListAsync();

        public async Task UpdateAsync(Baby model) {
            _babies.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<Baby> FindAsync(Expression<Func<Baby, bool>> predicate)
            => _babies.FirstOrDefaultAsync(predicate);
    }
}
