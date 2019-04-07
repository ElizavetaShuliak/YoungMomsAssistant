using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.Core.Repositories;

namespace YoungMomsAssistant.Core.Domain.Babies {
    public class BabyManager {

        private IRepository<User> _userInfoRepo;
        private IRepository<Baby> _babyRepo;
        private IRepository<BabyInfo> _babyInfoRepo;
    
        public BabyManager(
            IRepository<User> userRepository,
            IRepository<Baby> babyRepository,
            IRepository<BabyInfo> babyInfoRepository) {
            _userInfoRepo = userRepository;
            _babyRepo = babyRepository;
            _babyInfoRepo = babyInfoRepository;
        }

        public async Task AddNewBabyAsync(BabyDto babyDto, UserDto userDto) {
            var userDb = await _userInfoRepo
                .FindAsync(user => user.Email == user.Email);

            if (userDb == null) {
                return;
            }

            var baby = new Baby {
                FirstName = babyDto.FirstName,
                LastName = babyDto.LastName,
                BirthDay = babyDto.BirthDay
            };

            await _babyRepo.AddAsync(baby);

            var babyInfo = new BabyInfo {
                Baby = baby,
                BloodType = babyDto.BloodType,
                CurrentWeight = babyDto.CurrentWeight,
                CurrentGrowth = babyDto.CurrentGrowth,
                Sex = babyDto.Sex
            };

            await _babyInfoRepo.AddAsync(babyInfo);
        }
    }
}
