using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.DeveloperRepository;
using QoolloEDU.Database.Repositories.MediaContentRepository;
using QoolloEDU.Database.Repositories.NewsRepository;
using QoolloEDU.Database.Repositories.ProjectRepository;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Database.Repositories.RequestRepository;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;


namespace QoolloEDU.Database.Repositories.ProjectPageRepository
{
    public class ProjectPageRepository : IProjectPageRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IMediaContentRepository _mediaContentRepository;
        private readonly ITagRepository _tagRepository;

        public ProjectPageRepository(QoolloEduDbContext context,
            IProjectRepository projectRepository,
            IDeveloperRepository developerRepository,
            IRatingRepository ratingRepository,
            INewsRepository newsRepository,
            IRequestRepository requestRepository,
            IMediaContentRepository mediaContentRepository,
            ITagRepository tagRepository)
        {
            _context = context;
            _projectRepository = projectRepository;
            _developerRepository = developerRepository;
            _ratingRepository = ratingRepository;
            _newsRepository = newsRepository;
            _requestRepository = requestRepository;
            _mediaContentRepository = mediaContentRepository;
            _tagRepository = tagRepository;
        }
        
        public async Task<ProjectPageDto> GetProjectPageAsync(int projectId)
        {
            return new ProjectPageDto()
            {
                Project = await _projectRepository.GetProjectAsync(projectId),
                Owner = await GetOwnerAsync(projectId),
                Event = await GetEventByProjectIdAsync(projectId),
                MediaContent = await _mediaContentRepository.GetMediaContentAsync(projectId),
                Like = await _ratingRepository.GetProjectLikeAsync(projectId),
                Dislike = await _ratingRepository.GetProjectDislikeAsync(projectId),
                News = await _newsRepository.GetNewsAsync(projectId),
                Members = await _projectRepository.GetMembersAsync(projectId),
                Tags = await _tagRepository.GetProjectTagsAsync(projectId),
                Requests = await _requestRepository.GetRequestProjectUserAsync(projectId)
            };
        }
        
        private async Task<UserMiniDto> GetOwnerAsync(int projectId)
        {
            var project = await _context.Project.FirstAsync(p => p.Id == projectId);
            var developerId = project.OwnerId;
            return await _developerRepository.GetUserAsync(developerId);
        }

        private async Task<EventMiniDto> GetEventByProjectIdAsync(int projectId)
        {
            var project = await _context.Project
                .FirstAsync(p => p.Id == projectId);
            var eventId = project.EventId;

            if (eventId == null) return null;
            var event_ = await _context.Event
                .FirstAsync(e => e.Id == eventId);
            return new EventMiniDto()
            {
                Id = event_.Id,
                Name = event_.Name
            };

        }

        public async Task<int> PostRatingAsync(int developerId, int projectId, RatingType rating)
        {
            return await _ratingRepository.PostProjectRatingAsync(developerId, projectId, rating);
        }
        public async Task<int> PutRatingAsync(int developerId, int projectId, RatingType rating)
        {
            return await _ratingRepository.PutProjectRatingAsync(developerId, projectId, rating);
        }
        public async Task<int> DeleteRatingAsync(int developerId, int projectId)
        {
            return await _ratingRepository.DeleteProjectRatingAsync(developerId, projectId);
        }

        public async Task<int> UpdateAboutAsync(int projectId, MarkdownDto markdownDto)
        {
            return await _projectRepository.UpdateAboutAsync(projectId, markdownDto);
        }

        public async Task<int> PostNewsAsync(int projectId, AddNewsDto addNewsDto)
        {
            return await _newsRepository.PostNewsAsync(projectId, addNewsDto);
        }

        public async Task<List<MediaContentDto>> GetMediaContentAsync(int projectId)
        {
            return await _mediaContentRepository.GetMediaContentAsync(projectId);
        }
        public async Task AddMediaContentAsync(int projectId, MediaContentDto mediaContentDto)
        {
            await _mediaContentRepository.AddMediaContentAsync(projectId, mediaContentDto);
        }

        public async Task DeleteMediaContentAsync(int projectId, MediaContentDto mediaContentDto)
        {
            await _mediaContentRepository.DeleteMediaContentAsync(projectId, mediaContentDto);
        }
        
        public async Task<NewsPageDto> GetNewsPageAsync(int projectId, int newsId)
        {
           return await _newsRepository.GetNewsPageAsync(projectId, newsId);
        }
        
        public async Task<int> PostCommentAsync(int newsId, int developerId, CommentDto commentDto)
        {
            return await _newsRepository.PostCommentAsync(newsId, developerId, commentDto);
        }
        
        public async Task<RegistrationDto> CreateProjectAsync(int ownerId, ProjectRegDto projectRegDto)
        {
            return await _projectRepository.CreateProjectAsync(ownerId, projectRegDto);
        }
        
        public async Task<int> AddTagAsync(int developerId, TagDto tagDto)
        {
            return await _tagRepository.AddProjectTagAsync(developerId, tagDto);
        }
        
        public async Task<int> DeleteTagAsync(int developerId, TagDto tagDto)
        {
            return await _tagRepository.DeleteProjectTagAsync(developerId, tagDto);
        }

        public async Task<int> UpdateNameAsync(int projectId, NameDto nameDto)
        {
            return await _projectRepository.UpdateNameAsync(projectId, nameDto);
        }
    }
}