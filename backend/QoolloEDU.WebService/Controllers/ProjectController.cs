using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.ProjectPageRepository;
using QoolloEDU.Dto;
using QoolloEDU.WebService.Services;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController: Controller
    {
        private readonly IProjectPageRepository _projectPageRepository;
        private readonly AuthService _authService;
        private readonly ProjectService _projectService;
    
        public ProjectController(IProjectPageRepository projectPageRepository, AuthService authService, ProjectService projectService)
        {
            _projectPageRepository = projectPageRepository;
            _authService = authService;
            _projectService = projectService;
        }
    
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            IActionResult response;
            try
            {
                var projectPage = await _projectPageRepository.GetProjectPageAsync(projectId);
                
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '});
                if (token.Length > 1)
                {
                    var user = await _authService.DecodeRoleAsync(token[1]);
                    if (user.Role == (int) RoleType.Developer)
                    {
                        projectPage.RatingFlag = (int) await _projectService.GetProjectRatingFlagAsync(projectId, user.Id);
                        projectPage.MemberFlag = (int) await _projectService.GetMemberTypeAsync(projectId, user.Id);
                    }
                    else
                    {
                        projectPage.Requests = null;
                        projectPage.MemberFlag = (int) MemberType.NoAuth;
                        projectPage.RatingFlag = (int) FlagType.CantSet;
                    }
                }
                else
                {
                    projectPage.Requests = null;
                    projectPage.MemberFlag = (int) MemberType.NoAuth;
                    projectPage.RatingFlag = (int) FlagType.CantSet;
                }

                response = Ok(projectPage);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }

            return response;
        }

        [HttpPut("{projectId:int}/markdown")]
        public async Task<IActionResult> UpdateMarkDown(int projectId, [FromBody] MarkdownDto markdownDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                    await _projectPageRepository.UpdateAboutAsync(projectId, markdownDto);
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
        
        [HttpPut("{projectId:int}/name")]
        public async Task<IActionResult> UpdateName(int projectId, [FromBody] NameDto userDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                    await _projectPageRepository.UpdateNameAsync(projectId, userDto);
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
        
        [HttpPost("{projectId:int}/rating")]
        public async Task<IActionResult> PostRating(int projectId, [FromBody] RatingDto ratingDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && 
                    await _projectService.GetProjectRatingFlagAsync(projectId, user.Id) == FlagType.CanSet)
                    await _projectPageRepository.PostRatingAsync(user.Id, projectId, (RatingType) ratingDto.Rating);
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
        [HttpPut("{projectId:int}/rating")]
        public async Task<IActionResult> PutRating(int projectId, [FromBody] RatingDto ratingDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && 
                    (await _projectService.GetProjectRatingFlagAsync(projectId, user.Id) == FlagType.SetLike ||
                     await _projectService.GetProjectRatingFlagAsync(projectId, user.Id) == FlagType.SetDislike))
                    await _projectPageRepository.PutRatingAsync(user.Id, projectId, (RatingType) ratingDto.Rating);
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
        [HttpDelete("{projectId:int}/rating")]
        public async Task<IActionResult> DeleteRating(int projectId)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer&& 
                    (await _projectService.GetProjectRatingFlagAsync(projectId, user.Id) == FlagType.SetLike ||
                     await _projectService.GetProjectRatingFlagAsync(projectId, user.Id) == FlagType.SetDislike))
                    await _projectPageRepository.DeleteRatingAsync(user.Id, projectId);
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
        
        [HttpPost("{projectId:int}/news")]
        public async Task<IActionResult> PostNews(int projectId, [FromBody] AddNewsDto addNewsDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                {
                    var news = await _projectPageRepository.PostNewsAsync(projectId, addNewsDto);
                    response = StatusCode(StatusCodes.Status204NoContent);
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

        [HttpPost("{projectId:int}/media")]
        public async Task<IActionResult> PostProjectMediaContent(int projectId, [FromBody] MediaContentDto mediaContentDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                    await _projectPageRepository.AddMediaContentAsync(projectId, mediaContentDto);
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
        
        [HttpDelete("{projectId:int}/media")]
        public async Task<IActionResult> DeleteProjectMediaContent(int projectId, [FromBody] MediaContentDto mediaContentDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                    await _projectPageRepository.DeleteMediaContentAsync(projectId, mediaContentDto);
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
        
        [HttpGet("{projectId:int}/news/{newsId:int}")]
        public async Task<IActionResult> GetNewsPage(int projectId, int newsId)
        {
            IActionResult response;
            try
            {
                var newsPage = await _projectPageRepository.GetNewsPageAsync(projectId, newsId);
                response = Ok(newsPage);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProjectRegDto projectRegDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer)
                    response = Ok(await _projectPageRepository.CreateProjectAsync(user.Id, projectRegDto));
                else
                    throw new Exception();
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("{projectId:int}/tag")]
        public async Task<IActionResult> AddTag(int projectId, [FromBody] TagDto tagDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                    await _projectPageRepository.AddTagAsync(projectId, tagDto);
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
        
        [HttpDelete("{projectId:int}/tag")]
        public async Task<IActionResult> DeleteTag(int projectId, [FromBody] TagDto tagDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                    await _projectPageRepository.DeleteTagAsync(projectId, tagDto);
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
        
        [HttpPost("news/{newsId:int}/comment")]
        public async Task<IActionResult> AddComment (int newsId, [FromBody] CommentDto commentDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer)
                    await _projectPageRepository.PostCommentAsync(newsId, user.Id, commentDto);
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
    }
}