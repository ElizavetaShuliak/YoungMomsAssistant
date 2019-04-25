using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class UserRepository : IRepository<User> {

        private AppDbContext _dbContext;
        private DbSet<User> _users;

        public UserRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _users = _dbContext.Users;
        }

        public async Task AddAsync(User model) {
            _users.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _users.FindAsync(id);

            _users.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int id) => await _users.FindAsync(id);

        public async Task<List<User>> GetAsync() => await _users.ToListAsync();

        public async Task UpdateAsync(User model) {
            _users.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<User> FindAsync(Expression<Func<User, bool>> predicate) 
            => _users.FirstOrDefaultAsync(predicate);
    }
}
