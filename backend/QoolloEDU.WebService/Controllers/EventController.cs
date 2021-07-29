using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.EventPageRepository;
using QoolloEDU.Database.Repositories.EventRepository;
using QoolloEDU.Dto;
using QoolloEDU.WebService.Services;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController: Controller
    {
        private readonly IEventPageRepository _eventPageRepository;
        private readonly AuthService _authService;
        private readonly EventService _eventService;
        
        public EventController(IEventPageRepository eventPageRepository,
            AuthService authService, EventService eventService)
        {
            _eventPageRepository = eventPageRepository;
            _authService = authService;
            _eventService = eventService;
        }
        
        [HttpGet("{eventId:int}")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            IActionResult response;
            try
            {
                var _event = await _eventPageRepository.GetEventPageAsync(eventId);
                
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '});
                if (token.Length > 1)
                {
                    var user = await _authService.DecodeRoleAsync(token[1]);
                    if (user.Role == (int) RoleType.Developer)
                    {
                        _event.MemberFlag = (int) await _eventService.GetMemberTypeAsync(eventId, user.Id);
                        _event.RatingFlag = (int) await _eventService.GetEventRatingFlagAsync(eventId, user.Id);
                    }
                    else
                    {
                        _event.MemberFlag = (int) MemberType.NoAuth;
                        _event.RatingFlag = (int) FlagType.CantSet;
                    }
                }
                else
                {
                    _event.MemberFlag = (int) MemberType.NoAuth;
                    _event.RatingFlag = (int) FlagType.CantSet;
                }
                
                response = Ok(_event);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("{eventId:int}/rating")]
        public async Task<IActionResult> PostRating(int eventId, [FromBody] RatingDto ratingDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer &&
                    await _eventService.GetEventRatingFlagAsync(eventId, user.Id) == FlagType.CanSet)
                    await _eventPageRepository.PostEventRatingAsync(user.Id, eventId, (RatingType) ratingDto.Rating);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPut("{eventId:int}/rating")]
        public async Task<IActionResult> PutRating(int eventId, [FromBody] RatingDto ratingDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer &&
                    (await _eventService.GetEventRatingFlagAsync(eventId, user.Id) == FlagType.SetLike || 
                     await _eventService.GetEventRatingFlagAsync(eventId, user.Id) == FlagType.SetDislike))
                    await _eventPageRepository.PutEventRatingAsync(user.Id, eventId, (RatingType) ratingDto.Rating);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpDelete("{eventId:int}/rating")]
        public async Task<IActionResult> DeleteRating(int eventId)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer &&
                    (await _eventService.GetEventRatingFlagAsync(eventId, user.Id) == FlagType.SetLike || 
                     await _eventService.GetEventRatingFlagAsync(eventId, user.Id) == FlagType.SetDislike))
                    await _eventPageRepository.DeleteEventRatingAsync(user.Id, eventId);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("{eventId:int}/tag")]
        public async Task<IActionResult> AddTag(int eventId, [FromBody] TagDto tagDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Organizer && await _eventService.CheckOrganizer(eventId, user.Id))
                    await _eventPageRepository.AddTagAsync(eventId, tagDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpDelete("{eventId:int}/tag")]
        public async Task<IActionResult> DeleteTag(int eventId, [FromBody] TagDto tagDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Organizer && await _eventService.CheckOrganizer(eventId, user.Id))
                    await _eventPageRepository.DeleteTagAsync(eventId, tagDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            } 
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] EventRegDto eventRegDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Organizer)
                {
                    eventRegDto.OrganizerId = user.Id;
                    response = Ok(await _eventPageRepository.CreateEventAsync(eventRegDto));
                }
                else
                    throw new Exception();
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
    }
}