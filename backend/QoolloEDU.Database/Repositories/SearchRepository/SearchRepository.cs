using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.Repositories.ProjectPageRepository;
using QoolloEDU.Database.Repositories.ProjectRepository;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.SearchRepository
{
    public class SearchRepository: ISearchRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IRatingRepository _ratingRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProjectRepository _projectRepository;

        public SearchRepository(QoolloEduDbContext context, IRatingRepository ratingRepository, ITagRepository tagRepository, IProjectRepository projectRepository)
        {
            _context = context;
            _ratingRepository = ratingRepository;
            _tagRepository = tagRepository;
            _projectRepository = projectRepository;
        }
        
        public async Task<SearchProjectPageDto> GetProjectsAsync()
        {
            return new SearchProjectPageDto()
            {
                TopProjects = await GetTopProjectAsync(),
                Projects = await GetAllProjectAsync(),
                Tags = await _tagRepository.GetAllTagsAsync()
            };
        }
        
        public async Task<SearchEventPageDto> GetEventsAsync()
        {
            return new SearchEventPageDto()
            {
                TopEvents = await GetTopEventAsync(),
                Events = await GetAllEventAsync(),
                Tags = await _tagRepository.GetAllTagsAsync()
            };
        }
        
        public async Task<SearchDeveloperPageDto> GetDevelopersAsync()
        {
            return new SearchDeveloperPageDto()
            {
                Developers = await GetAllDeveloperAsync(),
                Tags = await _tagRepository.GetAllTagsAsync()
            };
        }

        private async Task<List<ProjectCardDto>> GetAllProjectAsync()
        {
            var projects = await _context.Project
                .ToListAsync();
            var result = new List<ProjectCardDto>();
            
            foreach (var project in projects)
            {
                var tmp = await _projectRepository.GetCardAsync(project.Id);
                var tags = await _context.ProjectTag
                    .Where(pt => pt.ProjectId == project.Id)
                    .Include(pt => pt.Tag)
                    .ToListAsync();
                var tags_ = tags.Select(tag => new TagDto() {Color = tag.Tag.Color, Id = tag.Tag.Id, Name = tag.Tag.Name}).ToList();
                tmp.Tags = tags_;
                result.Add(tmp);
            }
            //result.Sort((x, y) => -x.Rating.CompareTo(y.Rating));
            return result;
        }
        
        private async Task<List<ProjectCardMiniDto>> GetTopProjectAsync()
        {
            var projects = await _context.Project
                .ToListAsync();
            var result = new List<ProjectCardMiniDto>();
            
            foreach (var project in projects)
            {
                var org = await _context.Project
                    .Where(p => p.Id == project.Id)
                    .Where(p => p.EventId != null)
                    .Include(p => p.Event)
                    .Include(p => p.Event.Organizer)
                    .Include(p => p.Event.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
                
                result.Add(new ProjectCardMiniDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    OrganizerPhoto = org?.Event.Organizer.BaseUser.Photo,
                    Rating = await _ratingRepository.GetProjectLikeAsync(project.Id) - 
                             await _ratingRepository.GetProjectDislikeAsync(project.Id),
                });
            }
            result.Sort((x, y) => -x.Rating.CompareTo(y.Rating));
            return result.Count <= 10 ? result : result.Take(5).ToList();
        }
        
        private async Task<List<EventCardDto>> GetAllEventAsync()
        {
            var events = await _context.Event
                .ToListAsync();
            var result = new List<EventCardDto>();
            
            foreach (var eEvent in events)
            {
                var org = await _context.Event
                    .Where(e => e.Id == eEvent.Id)
                    .Include(e => e.Organizer)
                    .Include(e => e.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
                var tags = await _context.EventTag
                    .Where(e => e.EventId == eEvent.Id)
                    .Include(pt => pt.Tag)
                    .ToListAsync();
                var tagsDto = tags.Select(tag => new TagDto() {Color = tag.Tag.Color, Id = tag.Tag.Id, Name = tag.Tag.Name}).ToList();
                var proj = await _context.Project
                    .Where(p => p.EventId == eEvent.Id)
                    .ToListAsync();
                var members = await _context.DeveloperProject
                    .Include(dp => dp.Project)
                    .Where(dp => dp.Project.EventId == eEvent.Id)
                    .ToListAsync();
                
                result.Add(new EventCardDto()
                {
                    Id = eEvent.Id,
                    Name = eEvent.Name,
                    OrganizerName = org.Organizer.Name,
                    OrganizerPhoto = org.Organizer.BaseUser.Photo,
                    Description = eEvent.Description,
                    Rating = await _ratingRepository.GetEventLikeAsync(eEvent.Id) - 
                             await _ratingRepository.GetEventDislikeAsync(eEvent.Id),
                    ProjectCount = proj.Count,
                    MembersCount = members.Count,
                    StartDate = eEvent.StartDate,
                    EndDate = eEvent.EndDate,
                    Tags = tagsDto
                });
            }
            //result.Sort((x, y) => -x.Rating.CompareTo(y.Rating));
            return result;
        }
        
        private async Task<List<EventCardMiniDto>> GetTopEventAsync()
        {
            var events = await _context.Event
                .ToListAsync();
            var result = new List<EventCardMiniDto>();
            
            foreach (var eEvent in events)
            {
                var org = await _context.Event
                    .Where(e => e.Id == eEvent.Id)
                    .Include(e => e.Organizer)
                    .Include(e => e.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
                
                result.Add(new EventCardMiniDto()
                {
                    Id = eEvent.Id,
                    Name = eEvent.Name,
                    OrganizerPhoto = org.Organizer.BaseUser.Photo,
                    Rating = await _ratingRepository.GetEventLikeAsync(eEvent.Id) - 
                             await _ratingRepository.GetEventDislikeAsync(eEvent.Id),
                });
            }
            result.Sort((x, y) => -x.Rating.CompareTo(y.Rating));
            return result.Count <= 10 ? result : result.Take(5).ToList();
        }
        
        private async Task<List<UserCardDto>> GetAllDeveloperAsync()
        {
            var devs = await _context.Developer
                .ToListAsync();
            var result = new List<UserCardDto>();
            
            foreach (var dev in devs)
            {
                var tags = await _context.DeveloperTag
                    .Where(dt => dt.DeveloperId == dev.Id)
                    .Include(pt => pt.Tag)
                    .ToListAsync();
                var tagsDto = tags.Select(tag => new TagDto() {Color = tag.Tag.Color, Id = tag.Tag.Id, Name = tag.Tag.Name}).ToList();
                var baseUser = await _context.Developer
                    .Where(d => d.Id == dev.Id)
                    .Include(d => d.BaseUser)
                    .FirstOrDefaultAsync();
                
                result.Add(new UserCardDto()
                {
                    Photo = baseUser.BaseUser.Photo,
                    Nickname = dev.Nickname,
                    Name = dev.Name,
                    Surname = dev.Surname,
                    Age = DateTime.Now.DayOfYear < dev.BirthDate.DayOfYear ? 
                        DateTime.Now.Year - dev.BirthDate.Year + 1 :
                        DateTime.Now.Year - dev.BirthDate.Year,
                    Rating = await _ratingRepository.GetDeveloperRatingAsync(dev.Id),
                    Tags = tagsDto
                });
            }
            //result.Sort((x, y) => -x.Rating.CompareTo(y.Rating));
            return result;
        }
    }
}