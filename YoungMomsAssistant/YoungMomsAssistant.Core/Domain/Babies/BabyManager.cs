using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.Core.Repositories;

namespace YoungMomsAssistant.Core.Domain.Babies {
    public class BabyManager : IBabyManager {

        private IRepository<Baby> _babiesRepo;
        private IRepository<User> _usersRepo;

        public BabyManager(
            IRepository<Baby> babiesRepository,
            IRepository<User> usersRepository) {
            _babiesRepo = babiesRepository;
            _usersRepo = usersRepository;
        }

        public async Task AddNewBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var baby = new Baby {
                FirstName = babyDto.FirstName,
                LastName = babyDto.LastName,
                BirthDay = babyDto.BirthDay,
                BloodType = babyDto.BloodType,
                Sex = babyDto.Sex
            };

            baby.Users = new List<UserBaby> { new UserBaby {
                Baby = baby,
                User = owner
            } };

            await _babiesRepo.AddAsync(baby);
        }

        public async Task<IEnumerable<BabyDto>> GetBabiesByUserAsync(ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _babiesRepo
                .FindAllAsync(baby => baby.Users.FirstOrDefault(ub => ub.Baby_Id == baby.Id && ub.User_Id == owner.Id) != null))
                .Select(baby => new BabyDto {
                    Id = baby.Id,
                    FirstName = baby.FirstName,
                    LastName = baby.LastName,
                    BirthDay = baby.BirthDay,
                    BloodType = baby.BloodType,
                    Sex = baby.Sex
                });
        }

        public async Task UpdateBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var babyDb = await _babiesRepo.FindAsync(baby => baby.Id == babyDto.Id);
            if (babyDb.Users.FirstOrDefault(ub => ub.User_Id == owner.Id) != null) {
                babyDb.FirstName = babyDto.FirstName;
                babyDb.LastName = babyDto.LastName;
                babyDb.Sex = babyDto.Sex;
                babyDb.BirthDay = babyDto.BirthDay;
                babyDb.BloodType = babyDto.BloodType;

                await _babiesRepo.UpdateAsync(babyDb);
            }
            else {
                throw new ArgumentException("claimsPrincipal");
            }
        }

        private string GetEmailFromPrincipal(ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

        private async Task<User> GetOwnerAsync(string email)
            => await _usersRepo.FindAsync(user => user.Email == email)
                ?? throw new ArgumentException("claimsPrincipal");
    }
}
