using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.EventRepository;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.EventPageRepository
{
    public class EventPageRepository: IEventPageRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRatingRepository _ratingRepository;

        public EventPageRepository(QoolloEduDbContext context,
            IEventRepository eventRepository,
            ITagRepository tagRepository,
            IRatingRepository ratingRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
            _tagRepository = tagRepository;
            _ratingRepository = ratingRepository;
        }
        
        public async Task<EventPageDto> GetEventPageAsync(int eventId)
        {
            return new EventPageDto()
            {
                Organizer = await _eventRepository.GetOrganizerAsync(eventId),
                Event = await _eventRepository.GetEventAsync(eventId),
                ProjectNum = await _eventRepository.GetProjectNumAsync(eventId),
                Tags = await _tagRepository.GetEventTagsAsync(eventId),
                EventProjects = await _eventRepository.GetEventProjectsAsync(eventId),
                Like = await _ratingRepository.GetEventLikeAsync(eventId),
                Dislike = await _ratingRepository.GetEventDislikeAsync(eventId),
                ProjectPlaces = await _eventRepository.GetProjectPlacesAsync(eventId)
            };
        }

        public async Task<int> PutEventRatingAsync(int developerId, int eventId, RatingType rating)
        {
            return await _ratingRepository.PutEventRatingAsync(developerId, eventId, rating);
        }

        public async Task<int> PostEventRatingAsync(int developerId, int eventId, RatingType rating)
        {
            return await _ratingRepository.PostEventRatingAsync(developerId, eventId, rating);
        }

        public async Task<int> DeleteEventRatingAsync(int developerId, int eventId)
        {
            return await _ratingRepository.DeleteEventRatingAsync(developerId, eventId);
        }
        
        public async Task<int> AddTagAsync(int eventId, TagDto tagDto)
        {
            return await _tagRepository.AddEventTagAsync(eventId, tagDto);
        }
        
        public async Task<int> DeleteTagAsync(int eventId, TagDto tagDto)
        {
            return await _tagRepository.DeleteEventTagAsync(eventId, tagDto);
        }
        
        public async Task<RegistrationDto> CreateEventAsync(EventRegDto eventRegDto)
        {
            return await _eventRepository.CreateEventAsync(eventRegDto);
        }
    }
}