using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.ProjectRepository;

namespace QoolloEDU.WebService.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> CheckOwner(int developerId, int projectId)
        {
            return await _projectRepository.CheckOwner(developerId, projectId);
        }

        public async Task<FlagType> GetProjectRatingFlagAsync(int projectId, int userId)
        {
            return await _projectRepository.GetProjectRatingFlagAsync(projectId, userId);
        }

        public async Task<MemberType> GetMemberTypeAsync(int projectId, int developerId)
        {
            return await _projectRepository.GetMemberTypeAsync(projectId, developerId);
        }
    }
}