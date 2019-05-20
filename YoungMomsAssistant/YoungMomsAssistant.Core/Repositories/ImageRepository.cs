using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.Repositories {
    public class ImageRepository : IRepository<Image> {

        private AppDbContext _dbContext;
        private DbSet<Image> _images;

        public ImageRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
            _images = _dbContext.Images;
        }

        public async Task AddAsync(Image model) {
            _images.Add(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            var modelToRemove = await _images.FindAsync(id);

            _images.Remove(modelToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Image> GetAsync(int id) => await _images.FindAsync(id);

        public async Task<List<Image>> GetAsync() => await _images.ToListAsync();

        public async Task UpdateAsync(Image model) {
            _images.Attach(model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<Image> FindAsync(Expression<Func<Image, bool>> predicate)
            => _images.FirstOrDefaultAsync(predicate);

        public Task<List<Image>> FindAllAsync(Expression<Func<Image, bool>> predicate)
            => _images.Where(predicate).ToListAsync();
    }
}
