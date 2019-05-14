using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class LifeEventRepository : IRepository<LifeEvent> {

        private AppDbContext _dbContext;
        private DbSet<LifeEvent> _lifeEvents;

        public LifeEventRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _lifeEvents = _dbContext.LifeEvents;
        }

        public async Task AddAsync(LifeEvent model) {
            _lifeEvents.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _lifeEvents.FindAsync(id);

            _lifeEvents.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LifeEvent> GetAsync(int id) => await _lifeEvents.FindAsync(id);

        public async Task<List<LifeEvent>> GetAsync() => await _lifeEvents.ToListAsync();

        public async Task UpdateAsync(LifeEvent model) {
            _lifeEvents.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<LifeEvent> FindAsync(Expression<Func<LifeEvent, bool>> predicate)
            => _lifeEvents.Include("User").Include("Image").FirstOrDefaultAsync(predicate);

        public Task<List<LifeEvent>> FindAllAsync(Expression<Func<LifeEvent, bool>> predicate)
            => _lifeEvents.Include("User").Include("Image").Where(predicate).ToListAsync();
    }
}
