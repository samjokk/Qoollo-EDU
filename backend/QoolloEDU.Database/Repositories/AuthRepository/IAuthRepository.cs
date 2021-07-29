using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;


namespace QoolloEDU.Database.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<AccountDto> CheckLoginAsync(string email, string password);
        Task<RoleDto> GetRoleAsync(string email);
        Task<AuthDto> GetUserAsync(string email);
        Task<int> GetBaseUserIdAsync(RoleDto roleDto);
        
        Task<FlagType> GetDeveloperRatingFlagAsync(int developerId, int userId);
    }
}