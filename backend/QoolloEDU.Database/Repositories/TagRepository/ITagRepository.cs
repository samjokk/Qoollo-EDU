using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.TagRepository
{
    public interface ITagRepository
    {
        Task<List<TagDto>> GetAllTagsAsync();
        Task<List<TagDto>> GetDeveloperTagsAsync(int developerId);
        Task<List<TagDto>> GetProjectTagsAsync(int projectId);
        Task<List<TagDto>> GetEventTagsAsync(int eventId);
        Task<List<TagDto>> GetRequestTagsAsync(int requestId);
        
        Task AddTagDeveloperAsync(int developerId, TagDto tagDto);
        Task DeleteTagDeveloperAsync(int developerId, TagDto tagDto);
        
        Task<int> AddProjectTagAsync(int projectId, TagDto tagDto);
        Task<int> AddEventTagAsync(int eventId, TagDto tagDto);
        
        Task<int> DeleteProjectTagAsync(int projectId, TagDto tagDto);
        Task<int> DeleteEventTagAsync(int eventId, TagDto tagDto);
    }
}