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
        private IRepository<BabyGrowth> _babyGrowthsRepo;
        private IRepository<BabyWeight> _babyWeightsRepo;
        private IRepository<BabyVaccination> _babyVaccinationRepo;
        private IRepository<Image> _imagesRepo;

        public BabyManager(
            IRepository<Baby> babiesRepository,
            IRepository<User> usersRepository,
            IRepository<BabyGrowth> babyGrowthsRepo,
            IRepository<BabyWeight> babyWeightsRepo,
            IRepository<BabyVaccination> babyVaccinationRepo,
            IRepository<Image> imagesRepo) {

            _babiesRepo = babiesRepository;
            _usersRepo = usersRepository;
            _babyGrowthsRepo = babyGrowthsRepo;
            _babyWeightsRepo = babyWeightsRepo;
            _babyVaccinationRepo = babyVaccinationRepo;
            _imagesRepo = imagesRepo;
        }

        public async Task<BabyDto> AddNewBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var baby = new Baby {
                FirstName = babyDto.FirstName,
                LastName = babyDto.LastName,
                BirthDay = babyDto.BirthDay,
                BloodType = babyDto.BloodType,
                Sex = babyDto.Sex,
                Image = new Image { Source = babyDto.Image }
            };

            baby.UserBabies = new List<UserBaby> { new UserBaby {
                Baby = baby,
                User = owner
            } };

            await _babiesRepo.AddAsync(baby);
            babyDto.Id = baby.Id;

            return babyDto;
        }

        public async Task DeleteBabyAsync(int babyId, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var babyDb = await _babiesRepo.FindAsync(b => b.Id == babyId);

            if (babyDb.UserBabies.FirstOrDefault(ub => ub.User_Id == owner.Id) != null) {
                await _babiesRepo.RemoveAsync(babyId);
                await _imagesRepo.RemoveAsync(babyDb.Image_Id);
            }
            else {
                throw new ArgumentException("claimsPrincipal");
            }
        }

        public async Task<IEnumerable<BabyDto>> GetBabiesByUserAsync(ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _babiesRepo
                .FindAllAsync(baby => baby.UserBabies.FirstOrDefault(ub => ub.Baby_Id == baby.Id && ub.User_Id == owner.Id) != null))
                .Select(baby => new BabyDto {
                    Id = baby.Id,
                    FirstName = baby.FirstName,
                    LastName = baby.LastName,
                    BirthDay = baby.BirthDay,
                    BloodType = baby.BloodType,
                    Sex = baby.Sex,
                    Image = baby.Image.Source
                });
        }

        public async Task UpdateBabyAsync(BabyDto babyDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var babyDb = await _babiesRepo.FindAsync(baby => baby.Id == babyDto.Id);
            if (babyDb.UserBabies.FirstOrDefault(ub => ub.User_Id == owner.Id) != null) {
                babyDb.FirstName = babyDto.FirstName;
                babyDb.LastName = babyDto.LastName;
                babyDb.Sex = babyDto.Sex;
                babyDb.BirthDay = babyDto.BirthDay;
                babyDb.BloodType = babyDto.BloodType;

                if (babyDto.IsImageChanged) {
                    babyDb.Image = new Image { Source = babyDto.Image };
                }

                await _babiesRepo.UpdateAsync(babyDb);
            }
            else {
                throw new ArgumentException("claimsPrincipal");
            }
        }

        public async Task<BabyGrowthDto> AddBabyGrowthsAsync(BabyGrowthDto babyGrowthDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var babyDb = await _babiesRepo
                .FindAsync(b => b.Id == babyGrowthDto.BabyId);

            if (babyDb.UserBabies.FirstOrDefault(ub => ub.User_Id == owner.Id) != null) {
                var babyGrowthDb = await _babyGrowthsRepo.FindAsync(bg => bg.Date.Date == babyGrowthDto.Date.Date);

                if (babyGrowthDb != null) {
                    babyGrowthDb.Growth = babyGrowthDto.Growth;

                    await _babyGrowthsRepo.UpdateAsync(babyGrowthDb);
                    babyGrowthDto.Id = babyGrowthDb.Id;
                }
                else {
                    var babyGrowth = new BabyGrowth {
                        Baby = babyDb,
                        Date = babyGrowthDto.Date,
                        Growth = babyGrowthDto.Growth
                    };

                    await _babyGrowthsRepo.AddAsync(babyGrowth);
                    babyGrowthDto.Id = babyGrowth.Id;
                }
                
                return babyGrowthDto;
            }
            else {
                throw new ArgumentException("claimsPrincipal");
            }
        }

        public async Task<IEnumerable<BabyGrowthDto>> GetBabyGrowthsAsync(int babyId, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _babyGrowthsRepo
                .FindAllAsync(bg => bg.Baby.UserBabies
                    .FirstOrDefault(ub => ub.User_Id == owner.Id) != null && bg.Baby_Id == babyId))
                .Select(bg => new BabyGrowthDto {
                    Date = bg.Date,
                    Growth = bg.Growth,
                    Id = bg.Id,
                    BabyId = bg.Baby_Id
                });
        }

        public async Task<BabyWeightDto> AddBabyWeightsAsync(BabyWeightDto babyWeightDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var babyDb = await _babiesRepo
                .FindAsync(b => b.Id == babyWeightDto.BabyId);

            if (babyDb.UserBabies.FirstOrDefault(ub => ub.User_Id == owner.Id) != null) {
                var babyWeightDb = await _babyWeightsRepo.FindAsync(bw => bw.Date.Date == babyWeightDto.Date.Date);

                if (babyWeightDb != null) {
                    babyWeightDb.Weight = babyWeightDto.Weight;

                    await _babyWeightsRepo.UpdateAsync(babyWeightDb);
                    babyWeightDto.Id = babyWeightDb.Id;
                }
                else {
                    var babyWeight = new BabyWeight {
                        Baby = babyDb,
                        Date = babyWeightDto.Date,
                        Weight = babyWeightDto.Weight
                    };

                    await _babyWeightsRepo.AddAsync(babyWeight);
                    babyWeightDto.Id = babyWeight.Id;
                }

                return babyWeightDto;
            }
            else {
                throw new ArgumentException("claimsPrincipal");
            }
        }

        public async Task<IEnumerable<BabyWeightDto>> GetBabyWeigthsAsync(int babyId, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _babyWeightsRepo
                .FindAllAsync(bw => bw.Baby.UserBabies
                    .FirstOrDefault(ub => ub.User_Id == owner.Id) != null && bw.Baby_Id == babyId))
                .Select(bw => new BabyWeightDto {
                    Date = bw.Date,
                    Weight = bw.Weight,
                    Id = bw.Id,
                    BabyId = bw.Baby_Id
                });
        }

        public async Task AddBabyVaccinationAsync(BabyVaccinationDto babyVaccinationDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var babyDb = await _babiesRepo
                .FindAsync(b => b.Id == babyVaccinationDto.BabyId);

            if (babyDb.UserBabies.FirstOrDefault(ub => ub.User_Id == owner.Id) != null) {
                var babyVaccination = new BabyVaccination {
                    Baby = babyDb,
                    Date = babyVaccinationDto.Date,
                    Vaccination = new Vaccination {
                        Name = babyVaccinationDto.Name
                    }
                };

                await _babyVaccinationRepo.AddAsync(babyVaccination);
            }
            else {
                throw new ArgumentException("claimsPrincipal");
            }
        }

        public async Task<IEnumerable<BabyVaccinationDto>> GetBabyVaccinationsAsync(int babyId, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _babyVaccinationRepo
                .FindAllAsync(v => v.Baby.UserBabies
                    .FirstOrDefault(ub => ub.User_Id == owner.Id) != null && v.Baby_Id == babyId))
                .Select(bw => new BabyVaccinationDto {
                    BabyId = bw.Baby_Id,
                    Date = bw.Date,
                    Id = bw.Id,
                    Name = bw.Vaccination.Name
                });
        }

        private string GetEmailFromPrincipal(ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

        private async Task<User> GetOwnerAsync(string email)
            => await _usersRepo.FindAsync(user => user.Email == email)
                ?? throw new ArgumentException("claimsPrincipal");
    }
}
