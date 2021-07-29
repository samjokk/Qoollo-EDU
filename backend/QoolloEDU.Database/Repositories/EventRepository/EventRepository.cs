using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.EventRepository
{
    public class EventRepository: IEventRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IRatingRepository _ratingRepository;


        public EventRepository(QoolloEduDbContext context
        ,IRatingRepository ratingRepository)
        {
            _context = context;
            _ratingRepository = ratingRepository;
        }
        
        public async Task<List<ProjectCardDto>> GetEventProjectsAsync(int eventId)
        {
            var projects = await _context.Project
                .Where(p => p.EventId == eventId)
                .ToListAsync();
            var result = new List<ProjectCardDto>();

            foreach (var project in projects)
            {
                var members = await _context.DeveloperProject
                    .Where(dp => dp.ProjectId == project.Id)
                    .ToListAsync();
                var news = await _context.News
                    .Where(n => n.ProjectId == project.Id)
                    .ToListAsync();
                var org = await _context.Project
                    .Where(p => p.Id == project.Id)
                    .Include(p => p.Event)
                    .Include(p => p.Event.Organizer)
                    .Include(p => p.Event.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
                
                result.Add(new ProjectCardDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    OrganizerName = org.Event.Organizer.Name,
                    OrganizerPhoto = org.Event.Organizer.BaseUser.Photo,
                    Description = project.Description,
                    Rating = await _ratingRepository.GetProjectLikeAsync(project.Id) - 
                             await _ratingRepository.GetProjectDislikeAsync(project.Id),
                    MembersCount = members.Count,
                    NewsCount = news.Count,
                    CreationDate = project.CreationDate,
                });
            }
            return result;
        }

        public async Task<EventDto> GetEventAsync(int eventId)
        {
            var _event = await _context.Event
                .Where(e => e.Id == eventId)
                .FirstOrDefaultAsync();
            return new EventDto()
            {
                Name = _event.Name,
                Description = _event.Description,
                StartDate = _event.StartDate,
                EndDate = _event.EndDate
            };
        }
        
        public async Task<OrganizerMiniDto> GetOrganizerAsync(int eventId)
        {
            var organizer = await _context.Event
                .Where(e => e.Id == eventId)
                .Include(e => e.Organizer)
                .Include(e => e.Organizer.BaseUser)
                .FirstOrDefaultAsync();
            return new OrganizerMiniDto()
            {
                Id = organizer.OrganizerId,
                Name = organizer.Organizer.Name,
                Photo = organizer.Organizer.BaseUser.Photo
            };
        }
        
        public async Task<List<ProjectMiniDto>> GetProjectPlacesAsync(int eventId)
        {
            var projects = await _context.Project
                .Include(p => p.Event)
                .Where(p => p.Event.Id == eventId)
                .ToListAsync();
            
            return projects.Select(project => new ProjectMiniDto()
            {
                Id = project.Id,
                Name = project.Name,
                Place = (int) project.Place
            }).ToList();
        }

        public async Task<int> GetProjectNumAsync(int eventId)
        {
            var projects = await _context.Project
                .Include(p => p.Event)
                .Where(p => p.Event.Id == eventId)
                .ToListAsync();
            return projects.Count;
        }

        public async Task<FlagType> GetEventRatingFlagAsync(int eventId, int userId)
        {
            var rat = await _context.EventRating
                .Where(dr => dr.DeveloperId == userId)
                .Where(dr => dr.EventId == eventId)
                .FirstOrDefaultAsync();
            if (rat != null) return rat.Rating == RatingType.Like ? FlagType.SetLike : FlagType.SetDislike;
            return FlagType.CanSet;
        }
        
        public async Task<bool> CheckOrganizer(int eventId, int organizerId)
        {
            var event_ = await _context.Event
                .Where(e => e.OrganizerId == organizerId)
                .Where(e => e.Id == eventId)
                .FirstOrDefaultAsync();
            
            return event_ != null;
        }
        
        
        public async Task<RegistrationDto> CreateEventAsync(EventRegDto eventRegDto)
        {
            var event_ = await _context.AddAsync(new Event()
            {
                OrganizerId = eventRegDto.OrganizerId,
                Name = eventRegDto.Name,
                Description = eventRegDto.Markdown,
                StartDate = eventRegDto.StartDate,
                EndDate = eventRegDto.EndDate
            });
            await _context.SaveChangesAsync();

            foreach (var tag in eventRegDto.Tags)
            {
                await _context.AddAsync(new EventTag()
                {
                    EventId = event_.Entity.Id,
                    TagId = tag.Id,
                });
            }
            await _context.SaveChangesAsync();
            return new RegistrationDto()
            {
                ErrorCode = 0,
                Url = event_.Entity.Id.ToString()
            };
        }
        
        public async Task<List<EventCardDto>> GetPreviousEvents(int organizerId)
        {
            var events = await _context.Event
                .Where(e => e.OrganizerId == organizerId)
                .Where(e => e.EndDate <= DateTime.Now)
                .ToListAsync();
            var result = new List<EventCardDto>();
            
            foreach (var eEvent in events)
            {
                var org = await _context.Event
                    .Where(e => e.Id == eEvent.Id)
                    .Include(e => e.Organizer)
                    .Include(e => e.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
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
                });
            }

            return result;
        }
        
        public async Task<List<EventCardDto>> GetCurrentEvents(int organizerId)
        {
            var events = await _context.Event
                .Where(e => e.OrganizerId == organizerId)
                .Where(e => e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now)
                .ToListAsync();
            var result = new List<EventCardDto>();
            
            foreach (var eEvent in events)
            {
                var org = await _context.Event
                    .Where(e => e.Id == eEvent.Id)
                    .Include(e => e.Organizer)
                    .Include(e => e.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
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
                });
            }

            return result;
        }
        
        public async Task<List<EventCardDto>> GetUpcomingEvents(int organizerId)
        {
            var events = await _context.Event
                .Where(e => e.OrganizerId == organizerId)
                .Where(e => e.StartDate >= DateTime.Now)
                .ToListAsync();
            var result = new List<EventCardDto>();
            
            foreach (var eEvent in events)
            {
                var org = await _context.Event
                    .Where(e => e.Id == eEvent.Id)
                    .Include(e => e.Organizer)
                    .Include(e => e.Organizer.BaseUser)
                    .FirstOrDefaultAsync();
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
                });
            }

            return result;
        }
        public async Task<MemberType> GetMemberTypeAsync(int eventId, int developerId)
        {
            var member = await _context.DeveloperProject
                .Where(dp => dp.DeveloperId == developerId)
                .Include(dp => dp.Project)
                .Where(dp => dp.Project.EventId == eventId)
                .FirstOrDefaultAsync();
            if (member != null) return MemberType.AlreadySend;
            return MemberType.NotSent;
        }
    }
}