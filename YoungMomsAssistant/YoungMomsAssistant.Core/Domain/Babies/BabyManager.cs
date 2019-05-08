using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.Core.Repositories;

namespace YoungMomsAssistant.Core.Domain.Babies {
    public class BabyManager {

        private IRepository<Baby> _babyRepo;

        public BabyManager(
            IRepository<Baby> babyRepository) {
            _babyRepo = babyRepository;
        }

        public async Task AddNewBabyAsync(BabyDto babyDto) {
            var baby = new Baby {
                FirstName = babyDto.FirstName,
                LastName = babyDto.LastName,
                BirthDay = babyDto.BirthDay,
                BloodType = babyDto.BloodType,
                Sex = babyDto.Sex
            };

            await _babyRepo.AddAsync(baby);
        }
    }
}
