using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.EventRepository
{
    public interface IEventRepository
    {
        Task<List<ProjectCardDto>> GetEventProjectsAsync(int eventId);
        Task<EventDto> GetEventAsync(int eventId);
        Task<OrganizerMiniDto> GetOrganizerAsync(int eventId);
        Task<List<ProjectMiniDto>> GetProjectPlacesAsync(int eventId);
        Task<int> GetProjectNumAsync(int eventId);
        Task<FlagType> GetEventRatingFlagAsync(int eventId, int userId);
        Task<bool> CheckOrganizer(int eventId, int organizerId);
        Task<RegistrationDto> CreateEventAsync(EventRegDto eventRegDto);

        Task<List<EventCardDto>> GetPreviousEvents(int organizerId);
        Task<List<EventCardDto>> GetCurrentEvents(int organizerId);
        Task<List<EventCardDto>> GetUpcomingEvents(int organizerId);

        Task<MemberType> GetMemberTypeAsync(int eventId, int developerId);
    }
}