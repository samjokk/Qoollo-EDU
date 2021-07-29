using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.EventRepository;

namespace QoolloEDU.WebService.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        
        public async Task<bool> CheckOrganizer(int eventId, int organizerId)
        {
            return await _eventRepository.CheckOrganizer(eventId, organizerId);
        }
        
        public async Task<FlagType> GetEventRatingFlagAsync(int eventId, int userId)
        {
            return await _eventRepository.GetEventRatingFlagAsync(eventId, userId);
        }
        
        public async Task<MemberType> GetMemberTypeAsync(int eventId, int developerId)
        {
            return await _eventRepository.GetMemberTypeAsync(eventId, developerId);
        }
    }
}