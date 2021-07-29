using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.ProfilePageRepository
{
    public interface IProfilePageRepository
    {
        Task<int> GetDeveloperIdByNicknameAsync(string nickname);
        Task<ProfilePageDto> GetProfileAsync(int developerId);
        Task<List<UserCardDto>> GetAllProfileAsync();
        Task UpdateAboutAsync(int developerId, AboutDto aboutDto);
        Task AddTagAsync(int developerId, TagDto tagDto);
        Task DeleteTagAsync(int developerId, TagDto tagDto);

        Task AddLinkAsync(int developerId, LinkDto linkDto);
        Task DeleteLinkAsync(int developerId, LinkDto linkDto);

        Task PostRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating);
        Task PutRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating);
        Task DeleteRatingAsync(int developerIdFrom, int developerIdTo);
        
        Task<RegistrationDto> CreateAsync(UserRegDto userRegDto);
        Task PutPhotoAsync(int developerId, PhotoDto photoDto);
    }
}