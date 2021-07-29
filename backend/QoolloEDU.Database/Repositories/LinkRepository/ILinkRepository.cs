using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.LinkRepository
{
    public interface ILinkRepository
    {
        Task<List<LinkDto>> GetUsefulLinksAsync(int baseUserId);
        Task<List<LinkDto>> GetContactLinksAsync(int baseUserId);

        Task AddLinkAsync(int baseUserId, LinkDto linkDto);
        Task DeleteLinkAsync(int baseUserId, LinkDto linkDto);
    }
}