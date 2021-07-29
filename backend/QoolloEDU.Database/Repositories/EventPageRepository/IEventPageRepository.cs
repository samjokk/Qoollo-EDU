using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.EventPageRepository
{
    public interface IEventPageRepository
    {
        Task<EventPageDto> GetEventPageAsync(int eventId);

        Task<int> PutEventRatingAsync(int developerId, int eventId, RatingType rating);
        Task<int> PostEventRatingAsync(int developerId, int eventId, RatingType rating);
        Task<int> DeleteEventRatingAsync(int developerId, int eventId);

        Task<int> AddTagAsync(int eventId, TagDto tagDto);
        Task<int> DeleteTagAsync(int eventId, TagDto tagDto);
        
        Task<RegistrationDto> CreateEventAsync(EventRegDto eventRegDto);
    }
}