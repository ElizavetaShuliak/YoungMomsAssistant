using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class BabyVaccinationRepository : IRepository<BabyVaccination> {

        private AppDbContext _dbContext;
        private DbSet<BabyVaccination> _babyVaccination;

        public BabyVaccinationRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _babyVaccination = _dbContext.BabyVaccinations;
        }

        public async Task AddAsync(BabyVaccination model) {
            _babyVaccination.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _babyVaccination.FindAsync(id);

            _babyVaccination.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BabyVaccination> GetAsync(int id) => await _babyVaccination.FindAsync(id);

        public async Task<List<BabyVaccination>> GetAsync() => await _babyVaccination.ToListAsync();

        public async Task UpdateAsync(BabyVaccination model) {
            _babyVaccination.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<BabyVaccination> FindAsync(Expression<Func<BabyVaccination, bool>> predicate)
            => _babyVaccination.Include("Vaccination").Include("Baby").FirstOrDefaultAsync(predicate);

        public Task<List<BabyVaccination>> FindAllAsync(Expression<Func<BabyVaccination, bool>> predicate)
            => _babyVaccination.Include("Vaccination").Include("Baby").Where(predicate).ToListAsync();
    }
}
