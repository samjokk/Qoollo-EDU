using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.DeveloperRepository;
using QoolloEDU.Database.Repositories.LinkRepository;
using QoolloEDU.Database.Repositories.ProjectRepository;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Database.Repositories.RequestRepository;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.ProfilePageRepository
{
    public class ProfilePageRepository: IProfilePageRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly ITagRepository _tagRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ILinkRepository _linkRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRequestRepository _requestRepository;
        
        public ProfilePageRepository(QoolloEduDbContext context, 
            ITagRepository tagRepository,
            ILinkRepository linkRepository,
            IRatingRepository ratingRepository,
            IDeveloperRepository developerRepository,
            IProjectRepository projectRepository,
            IRequestRepository requestRepository)
        {
            _context = context;
            _tagRepository = tagRepository;
            _ratingRepository = ratingRepository;
            _ratingRepository = ratingRepository;
            _linkRepository = linkRepository;
            _developerRepository = developerRepository;
            _projectRepository = projectRepository;
            _requestRepository = requestRepository;
        }

        public async Task<ProfilePageDto> GetProfileAsync(int developerId)
        {
            var dev = await _context.Developer
                .Where(d => d.Id == developerId)
                .FirstOrDefaultAsync();
            var baseUserId = dev.UserId;
            return new ProfilePageDto()
                {
                    Developer = await _developerRepository.GetDeveloperAsync(developerId),
                    Rating = await _ratingRepository.GetDeveloperRatingAsync(developerId),
                    Tags = await _tagRepository.GetDeveloperTagsAsync(developerId),
                    UsefulLinks = await _linkRepository.GetUsefulLinksAsync(baseUserId),
                    ContactLinks = await _linkRepository.GetContactLinksAsync(baseUserId),
                    ProjectsEvent = await _projectRepository.GetProjectEventAsync(developerId),
                    ProjectsNoEvent = await _projectRepository.GetProjectNoEventAsync(developerId),
                    Achievements = await _developerRepository.GetDeveloperAchievementsAsync(developerId),
                    Requests = await _requestRepository.GetRequestDeveloperAsync(developerId),
                }; 
        }

        public async Task<List<UserCardDto>> GetAllProfileAsync()
        {
            var devs = await _context.Developer
                .Include(d => d.BaseUser)
                .ToListAsync();
            var result = new List<UserCardDto>();
            foreach (var dev in devs)
            {
                var rating = await _ratingRepository.GetDeveloperRatingAsync(dev.Id);
                var tags = await _tagRepository.GetDeveloperTagsAsync(dev.Id);
                
                result.Add(new UserCardDto()
                {
                    Nickname = dev.Nickname,
                    Name = dev.Name,
                    Surname = dev.Surname,
                    Photo = dev.BaseUser.Photo,
                    Age = DateTime.Now.DayOfYear < dev.BirthDate.DayOfYear ? 
                        DateTime.Now.Year - dev.BirthDate.Year + 1 :
                        DateTime.Now.Year - dev.BirthDate.Year,
                    Rating = rating,
                    Tags = tags
                });
            }

            return result;
        }

        public async Task UpdateAboutAsync(int developerId, AboutDto aboutDto)
        {
            var developer = await _context.Developer
                .Include(d => d.BaseUser)
                .FirstOrDefaultAsync(d => d.Id == developerId);
            
            developer.BaseUser.About = aboutDto.NewAbout;
            await _context.SaveChangesAsync();
        }

        public async Task AddTagAsync(int developerId, TagDto tagDto)
        {
            await _tagRepository.AddTagDeveloperAsync(developerId, tagDto);
        }
        
        public async Task DeleteTagAsync(int developerId, TagDto tagDto)
        {
            await _tagRepository.DeleteTagDeveloperAsync(developerId, tagDto);
        }

        public async Task AddLinkAsync(int baseUserId, LinkDto linkDto)
        {
            await _linkRepository.AddLinkAsync(baseUserId, linkDto);
        }
        public async Task DeleteLinkAsync(int baseUserId, LinkDto linkDto)
        {
            await _linkRepository.DeleteLinkAsync(baseUserId, linkDto);
        }

        public async Task PostRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating)
        {
            await _ratingRepository.PostDeveloperRatingAsync(developerIdFrom, developerIdTo, rating);
        }
        public async Task PutRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating)
        {
            await _ratingRepository.PutDeveloperRatingAsync(developerIdFrom, developerIdTo, rating);
        }
        public async Task DeleteRatingAsync(int developerIdFrom, int developerIdTo)
        {
            await _ratingRepository.DeleteDeveloperRatingAsync(developerIdFrom, developerIdTo);
        }

        public async Task<RegistrationDto> CreateAsync(UserRegDto userRegDto)
        {
            return await _developerRepository.CreateAsync(userRegDto);
        }

        public async Task PutPhotoAsync(int developerId, PhotoDto photoDto)
        {
            await _developerRepository.PutPhotoAsync(developerId, photoDto);
        }

        public async Task<int> GetDeveloperIdByNicknameAsync(string nickname)
        {
            var developer = await _context.Developer
                .Where(d => d.Nickname == nickname)
                .FirstOrDefaultAsync();
            return developer.Id;
        }
    }
}