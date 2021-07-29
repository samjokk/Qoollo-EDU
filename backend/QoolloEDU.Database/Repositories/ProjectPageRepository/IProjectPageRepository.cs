using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.ProjectPageRepository
{
    public interface IProjectPageRepository
    {
        Task<ProjectPageDto> GetProjectPageAsync(int projectId);
        Task<int> PostRatingAsync(int developerId, int projectId, RatingType rating);
        Task<int> PutRatingAsync(int developerId, int projectId, RatingType rating);
        Task<int> DeleteRatingAsync(int developerId, int projectId);
        Task<int> UpdateAboutAsync(int projectId, MarkdownDto markdownDto);
        Task<int> PostNewsAsync(int projectId, AddNewsDto addNewsDto);
        
        Task<List<MediaContentDto>> GetMediaContentAsync(int projectId);
        Task AddMediaContentAsync(int projectId, MediaContentDto mediaContentDto);
        Task DeleteMediaContentAsync(int projectId, MediaContentDto mediaContentDto);
        Task<NewsPageDto> GetNewsPageAsync(int projectId, int newsId);
        Task<int> PostCommentAsync(int newsId, int developerId, CommentDto commentDto);
        Task<int> UpdateNameAsync(int projectId, NameDto nameDto);
        Task<RegistrationDto> CreateProjectAsync(int ownerId, ProjectRegDto projectRegDto);

        Task<int> AddTagAsync(int developerId, TagDto tagDto);
        Task<int> DeleteTagAsync(int developerId, TagDto tagDto);
    }
}