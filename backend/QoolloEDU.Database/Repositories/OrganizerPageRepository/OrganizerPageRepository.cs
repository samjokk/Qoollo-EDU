using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.EventRepository;
using QoolloEDU.Database.Repositories.LinkRepository;
using QoolloEDU.Database.Repositories.OrganizerRepository;
using QoolloEDU.Database.Repositories.RatingRepository;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.OrganizerPageRepository
{
    public class OrganizerPageRepository: IOrganizerPageRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly ILinkRepository _linkRepository;
        private readonly IOrganizerRepository _organizerRepository;
        
        public OrganizerPageRepository(QoolloEduDbContext context,
            IOrganizerRepository organizerRepository,
            IEventRepository eventRepository,
            ILinkRepository linkRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
            _linkRepository = linkRepository;
            _organizerRepository = organizerRepository;
        }
        
        public async Task<OrganizerPageDto> GetOrganizerPageAsync(int orgId)
        {
            var org = await _context.Organizer
                .Where(o => o.Id == orgId)
                .FirstOrDefaultAsync();
            return new OrganizerPageDto()
            {
                Organizer = await _organizerRepository.GetOrganizerAsync(orgId),
                PreviousEvents = await _eventRepository.GetPreviousEvents(orgId),
                CurrentEvents = await _eventRepository.GetCurrentEvents(orgId),
                UpcomingEvents = await _eventRepository.GetUpcomingEvents(orgId),
                ContactLinks = await _linkRepository.GetContactLinksAsync(org.UserId)
            };
        }

        public async Task<RegistrationDto> CreateOrganizerAsync(OrganizerRegDto organizerRegDto)
        {
            return await _organizerRepository.CreateOrganizerAsync(organizerRegDto);
        }
        
    }
}