using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.ProjectRepository
{
    public interface IProjectRepository
    {
        Task<ProjectCardDto> GetCardAsync(int projectId);
        Task<List<ProjectCardDto>> GetProjectEventAsync(int developerId);
        Task<List<ProjectCardDto>> GetProjectNoEventAsync(int developerId);
        Task<ProjectDto> GetProjectAsync(int projectId);
        Task<List<UserMiniDto>> GetMembersAsync(int projectId);
        Task<bool> CheckOwner(int developerId, int projectId);
        Task<FlagType> GetProjectRatingFlagAsync(int projectId, int userId);

        Task<int> UpdateAboutAsync(int projectId, MarkdownDto markdownDto);
        Task<int> UpdateNameAsync(int projectId, NameDto nameDto);
        Task<RegistrationDto> CreateProjectAsync(int ownerId, ProjectRegDto projectRegDto);

        Task<MemberType> GetMemberTypeAsync(int projectId, int developerId);
    }
}