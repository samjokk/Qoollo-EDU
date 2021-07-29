using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.DeveloperRepository
{
    public interface IDeveloperRepository
    {
        Task<UserDto> GetDeveloperAsync(int developerId);
        Task<UserMiniDto> GetUserAsync(int developerId);
        Task<List<AchievementDto>> GetDeveloperAchievementsAsync(int developerId);
        Task<RegistrationDto> CreateAsync(UserRegDto userRegDto);
        Task PutPhotoAsync(int developerId, PhotoDto photoDto);
    }
}