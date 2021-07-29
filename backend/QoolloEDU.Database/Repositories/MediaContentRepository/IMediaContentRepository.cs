using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.MediaContentRepository
{
    public interface IMediaContentRepository
    {
        Task<List<MediaContentDto>> GetMediaContentAsync(int projectId);
        Task AddMediaContentAsync(int projectId, MediaContentDto mediaContentDto);
        Task DeleteMediaContentAsync(int projectId, MediaContentDto mediaContentDto);
    }
}