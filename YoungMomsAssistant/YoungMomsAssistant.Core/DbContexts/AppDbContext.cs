using System.Data.Entity;
using YoungMomsAssistant.Core.Models.DbModels;

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

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<BabyDisease> BabyDiseases { get; set; }

        public DbSet<Vaccination> Vaccinations { get; set; }

        public DbSet<BabyVaccination> BabyVaccinations { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<LifeEvent> LifeEvents { get; set; }

        public DbSet<OralCavity> OralCavities { get; set; }
    }
}
