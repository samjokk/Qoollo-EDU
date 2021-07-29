using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.NewsRepository
{
    public interface INewsRepository
    {
        Task<List<NewsDto>> GetNewsAsync(int projectId);
        Task<int> PostNewsAsync(int projectId, AddNewsDto addNewsDto);
        Task<NewsPageDto> GetNewsPageAsync(int projectId, int newsId);
        Task<int> PostCommentAsync(int newsId, int developerId, CommentDto commentDto);
    }
}