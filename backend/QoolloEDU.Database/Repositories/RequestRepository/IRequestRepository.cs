using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.RequestRepository
{
    public interface IRequestRepository
    {
        Task<List<RequestDto>> GetRequestProjectAsync(int projectId);
        Task<List<RequestDto>> GetRequestDeveloperAsync(int developerId);
        Task<int> AcceptRequestAsync(int developerId, int projectId);
        Task<int> DeclineRequestAsync(int developerId, int projectId);
        Task<int> PostRequestFromDeveloperAsync(RequestDto requestDto);
        Task<int> PostRequestFromProjectAsync(RequestDto requestDto);
        Task<List<ProjectRequestDto>> GetRequestProjectUserAsync(int projectId);
    }
}