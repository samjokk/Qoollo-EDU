using System.Threading.Tasks;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.SearchRepository
{
    public interface ISearchRepository
    {
        Task<SearchProjectPageDto> GetProjectsAsync();
        Task<SearchEventPageDto> GetEventsAsync();
        Task<SearchDeveloperPageDto> GetDevelopersAsync();
    }
}