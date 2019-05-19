using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Models.DtoModels;
using YoungMomsAssistant.Core.Repositories;

namespace YoungMomsAssistant.Core.Domain.LifeEvents {
    public class LifeEventManager : ILifeEventManager {

        private IRepository<LifeEvent> _LifeEventsRepo;
        private IRepository<User> _usersRepo;
        private IRepository<Image> _imagesRepo;

        public LifeEventManager(
            IRepository<LifeEvent> lifeEventRepository,
            IRepository<User> usersRepository,
            IRepository<Image> iamgesRepo) {

            _LifeEventsRepo = lifeEventRepository;
            _usersRepo = usersRepository;
            _imagesRepo = iamgesRepo;
        }

        public async Task AddNewLifeEventAsync(LifeEventDto lifeEventDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var image = new Image {
                Source = lifeEventDto.Image
            };

            await _imagesRepo.AddAsync(image);

            var lifeEvent = new LifeEvent {
                Title = lifeEventDto.Title,
                Summary = lifeEventDto.Summary,
                Date = lifeEventDto.Date,
                User = owner,
                Image = image,
            };

            await _LifeEventsRepo.AddAsync(lifeEvent);
        }

        public async Task<IEnumerable<LifeEventDto>> GetLifeEventsByUserAsync(ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _LifeEventsRepo
                .FindAllAsync(lifeEvent => lifeEvent.User_Id == owner.Id))
                .Select(lifeEvent => new LifeEventDto {
                    Id = lifeEvent.Id,
                    Title = lifeEvent.Title,
                    Summary = lifeEvent.Summary,
                    Image = lifeEvent.Image.Source,
                    Date = lifeEvent.Date
                });
        }

        public async Task<IEnumerable<LifeEventDto>> GetLifeEventsByDateAsync(ClaimsPrincipal claimsPrincipal, DateTime date) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            return (await _LifeEventsRepo
                .FindAllAsync(lifeEvent => lifeEvent.User_Id == owner.Id && lifeEvent.Date.Date == date.Date))
                .Select(lifeEvent => new LifeEventDto {
                    Id = lifeEvent.Id,
                    Title = lifeEvent.Title,
                    Summary = lifeEvent.Summary,
                    Image = lifeEvent.Image.Source,
                    Date = lifeEvent.Date
                });
        }

        public async Task UpdateLifeEventAsync(LifeEventDto lifeEventDto, ClaimsPrincipal claimsPrincipal) {
            var email = GetEmailFromPrincipal(claimsPrincipal);
            var owner = await GetOwnerAsync(email);

            var lifeEventDb = await _LifeEventsRepo.FindAsync(lifeEvent => lifeEvent.Id == lifeEventDto.Id);
            if (lifeEventDb.User_Id == owner.Id) {
                lifeEventDb.Title = lifeEventDto.Title;
                lifeEventDb.Summary = lifeEventDto.Summary;
                lifeEventDb.Date = lifeEventDto.Date;
                if (lifeEventDto.IsImageChanged) {
                    lifeEventDb.Image = new Image { Source = lifeEventDto.Image };
                }

                await _LifeEventsRepo.UpdateAsync(lifeEventDb);
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
