using Microsoft.EntityFrameworkCore;
using YoungMomsAssistant.Core.Models.DbModels;

namespace YoungMomsAssistant.Core.DbContexts {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Baby> Babies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<BabyDisease> BabyDiseases { get; set; }

        public DbSet<Vaccination> Vaccinations { get; set; }

        public DbSet<BabyVaccination> BabyVaccinations { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<LifeEvent> LifeEvents { get; set; }

        public DbSet<OralCavity> OralCavities { get; set; }

        public DbSet<BabyAllergy> BabyAllergies { get; set; }

        public DbSet<UserBaby> UserBabies { get; set; }

        public DbSet<BabyWeight> BabyWeights { get; set; }

        public DbSet<BabyGrowth> BabyGrowths { get; set; }
    }
}
