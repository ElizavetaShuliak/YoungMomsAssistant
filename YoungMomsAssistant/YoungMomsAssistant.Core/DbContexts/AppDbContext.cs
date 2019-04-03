using System.Data.Entity;
using YoungMomsAssistant.Core.DbModels;

namespace YoungMomsAssistant.Core.DbContexts {
    public class AppDbContext : DbContext {

        public AppDbContext()
            : base("name=DefaultConnection") {
        }

        public DbSet<Baby> Babies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<BabyInfo> BabyInfos { get; set; }

        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<Sex> Sexes { get; set; }
    }
}
