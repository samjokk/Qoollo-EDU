using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController: Controller
    {
        
        private readonly ITagRepository _tagRepository;
        
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> AllTag()
        {
            IActionResult response;
            try
            {
                var tags = await _tagRepository.GetAllTagsAsync();
                response = Ok(tags);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
    }
}