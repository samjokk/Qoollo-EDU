using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.Repositories.OrganizerPageRepository;
using QoolloEDU.Dto;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizerController: Controller
    {
        private readonly IOrganizerPageRepository _organizer;

        public OrganizerController(IOrganizerPageRepository organizer)
        {
            _organizer = organizer;
        }
        
        [HttpGet("{orgId:int}")]
        public async Task<IActionResult> GetOrganizerPage(int orgId)
        {
            IActionResult response;
            try
            {
                var profiles = await _organizer.GetOrganizerPageAsync(orgId);
                response = Ok(profiles);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create(OrganizerRegDto organizerRegDto)
        {
            IActionResult response;
            try
            {
                var profiles = await _organizer.CreateOrganizerAsync(organizerRegDto);
                response = Ok(profiles);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
    }
}