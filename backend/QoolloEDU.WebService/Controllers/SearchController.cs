using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.Repositories.SearchRepository;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController: Controller
    {
        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [HttpGet("projects")]
        public async Task<IActionResult> SearchProject()
        {
            IActionResult response;
            try
            {
                var projects = await _searchRepository.GetProjectsAsync();
                response = Ok(projects);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }
        
        [HttpGet("events")]
        public async Task<IActionResult> SearchEvent()
        {
            IActionResult response;
            try
            {
                var projects = await _searchRepository.GetEventsAsync();
                response = Ok(projects);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }
        
        [HttpGet("developers")]
        public async Task<IActionResult> SearchDeveloper()
        {
            IActionResult response;
            try
            {
                var projects = await _searchRepository.GetDevelopersAsync();
                response = Ok(projects);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

    }
}