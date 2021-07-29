using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.ProjectRepository
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IRatingRepository _ratingRepository;

        public ProjectRepository(QoolloEduDbContext context, IRatingRepository ratingRepository)
        {
            _context = context;
            _ratingRepository = ratingRepository;
        }

        public async Task<ProjectCardDto> GetCardAsync(int projectId)
        {
            var project = await _context.Project
                .Where(p => p.Id == projectId)
                .FirstOrDefaultAsync();
            var members = await _context.DeveloperProject
                .Where(dp => dp.ProjectId == projectId)
                .ToListAsync();
            var news = await _context.News
                .Where(n => n.ProjectId == projectId)
                .ToListAsync();
            var org = await _context.Project
                .Where(p => p.Id == projectId)
                .Where(p => p.EventId != null)
                .Include(p => p.Event)
                .Include(p => p.Event.Organizer)
                .Include(p => p.Event.Organizer.BaseUser)
                .FirstOrDefaultAsync();
                
            return new ProjectCardDto()
            {
                Id = project.Id,
                Name = project.Name,
                OrganizerName = org?.Event.Organizer.Name,
                OrganizerPhoto = org?.Event.Organizer.BaseUser.Photo,
                Description = project.Description,
                Rating = await _ratingRepository.GetProjectLikeAsync(project.Id) - 
                         await _ratingRepository.GetProjectDislikeAsync(project.Id),
                MembersCount = members.Count,
                NewsCount = news.Count,
                CreationDate = project.CreationDate,
            };
        }

        public async Task<List<ProjectCardDto>> GetProjectEventAsync(int developerId)
        {
            var projects = await _context.DeveloperProject
                .Where(dp => dp.DeveloperId == developerId)
                .Include(dp => dp.Project)
                .Where(dp => dp.Project.EventId != null)
                .ToListAsync();
            var result = new List<ProjectCardDto>();

            foreach (var project in projects)
            {
                result.Add(await GetCardAsync(project.ProjectId));
            }
            return result;
        }

        public async Task<List<ProjectCardDto>> GetProjectNoEventAsync(int developerId)
        {
            var projects = await _context.DeveloperProject
                .Where(dp => dp.DeveloperId == developerId)
                .Include(dp => dp.Project)
                .Where(dp => dp.Project.EventId == null)
                .ToListAsync();
            var result = new List<ProjectCardDto>();

            foreach (var project in projects)
            {
                result.Add(await GetCardAsync(project.ProjectId));
            }
            return result;
        }

        public async Task<ProjectDto> GetProjectAsync(int projectId)
        {
            var project = await _context.Project
                .Where(p => p.Id == projectId)
                .FirstOrDefaultAsync();

            return new ProjectDto()
                {
                    Name = project.Name, 
                    Markdown = project.Markdown,
                    CreationDate = project.CreationDate
                };
        }
        
        public async Task<List<UserMiniDto>> GetMembersAsync(int projectId)
        {
            var developers = await _context.DeveloperProject
                .Where(dp => dp.ProjectId == projectId)
                .Include(dp => dp.Developer.BaseUser)
                .ToListAsync();

            return developers.Select(developer => new UserMiniDto()
                {
                    Name = developer.Developer.Name, 
                    Photo = developer.Developer.BaseUser.Photo, 
                    Nickname = developer.Developer.Nickname, 
                    Surname = developer.Developer.Surname
                    
                }).ToList();
        }

        public async Task<bool> CheckOwner(int developerId, int projectId)
        {
            var pr = await _context.Project
                .Where(p => p.OwnerId == developerId)
                .Where(p => p.Id == projectId)
                .FirstOrDefaultAsync();
            
            return pr != null;
        }
        
        public async Task<FlagType> GetProjectRatingFlagAsync(int projectId, int userId)
        {
            var rat = await _context.ProjectRating
                .Where(dr => dr.DeveloperId == userId)
                .Where(dr => dr.ProjectId == projectId)
                .FirstOrDefaultAsync();
            if (rat != null) return rat.Rating == RatingType.Like ? FlagType.SetLike : FlagType.SetDislike;
            return FlagType.CanSet;
        }
        
        public async Task<int> UpdateAboutAsync(int projectId, MarkdownDto markdownDto)
        {
            var project = await _context.Project
                .FirstOrDefaultAsync(p => p.Id == projectId);
            
            project.Markdown = markdownDto.NewMarkdown;
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> UpdateNameAsync(int projectId, NameDto nameDto)
        {
            var project = await _context.Project
                .FirstOrDefaultAsync(p => p.Id == projectId);
            
            project.Name = nameDto.NewName;
            return await _context.SaveChangesAsync();
        }
        
        public async Task<RegistrationDto> CreateProjectAsync(int ownerId, ProjectRegDto projectRegDto)
        {
            var project = await _context.AddAsync(new Project()
                {
                    OwnerId = ownerId,
                    Name = projectRegDto.Name,
                    Markdown = projectRegDto.Markdown,
                    EventId = projectRegDto.EventId,    
                    CreationDate = DateTime.Now,
                    Place = projectRegDto.EventId != null ? PlaceType.Participant: (PlaceType?) null
                });
            
            await _context.SaveChangesAsync();
            
            await _context.AddAsync(new DeveloperProject()
            {
                DeveloperId = ownerId,
                ProjectId = project.Entity.Id,
            });
            
            await _context.SaveChangesAsync();
            
            if (projectRegDto.Tags != null)
                foreach (var tag in projectRegDto.Tags)
                    await _context.AddAsync(new ProjectTag()
                    {
                        ProjectId = project.Entity.Id,
                        TagId = tag.Id,
                    });

            if (projectRegDto.MediaContent != null)
                foreach (var media in projectRegDto.MediaContent)
                    await _context.AddAsync(new MediaContent()
                    {
                        ProjectId = project.Entity.Id,
                        Type = (MediaType) media.Type,
                        URL = media.Link
                    });

            await _context.SaveChangesAsync();

            return new RegistrationDto()
            {
                ErrorCode = (int) CreateProjectErrorType.Ok,
                Url = project.Entity.Id.ToString()
            };
        }

        public async Task<MemberType> GetMemberTypeAsync(int projectId, int developerId)
        {
            var member = await _context.DeveloperProject
                .Where(dp => dp.ProjectId == projectId)
                .Where(dp => dp.DeveloperId == developerId)
                .FirstOrDefaultAsync();
            if (member != null) return MemberType.AlreadyMember;
            
            var request = await _context.Request
                .Where(r => r.Type == RequestType.DirectToProject)
                .Where(r => r.DeveloperId == developerId)
                .Where(r => r.ProjectId == projectId)
                .FirstOrDefaultAsync();
            if (request != null) return MemberType.AlreadySend;
            return MemberType.NotSent;
        }
    }
}