using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.RequestRepository;
using QoolloEDU.Dto;
using QoolloEDU.WebService.Services;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController: Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly AuthService _authService;
        private readonly ProjectService _projectService;
        
        public RequestController(IRequestRepository requestRepository, 
            AuthService authService, 
            ProjectService projectService)
        {
            _requestRepository = requestRepository;
            _authService = authService;
            _projectService = projectService;
        }
        
        [HttpGet("project/{projectId:int}")]
        public async Task<IActionResult> GetProjectRequests(int projectId)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, projectId))
                {
                    var requests = await _requestRepository.GetRequestProjectAsync(projectId);
                    response = Ok(requests);
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
        
        [HttpGet("developer")]
        public async Task<IActionResult> GetDeveloperRequests()
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer)
                {
                    var requests = await _requestRepository.GetRequestDeveloperAsync(user.Id);
                    response = Ok(requests);
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
        
        [HttpDelete("decline")]
        public async Task<IActionResult> DeclineRequest([FromBody]RequestDto requestDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                switch (requestDto.Type)
                {
                    case (int) RequestType.DirectToProject:
                        if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, requestDto.ProjectId))
                        {
                            var val = await _requestRepository.DeclineRequestAsync(requestDto.DeveloperId, requestDto.ProjectId);
                            response = Ok(val);
                        }
                        else
                            throw new Exception();
                        break;
                    case (int) RequestType.DirectToUser:
                        if (user.Role == (int) RoleType.Developer && user.Id == requestDto.DeveloperId)
                        {
                            var val = await _requestRepository.DeclineRequestAsync(user.Id, requestDto.ProjectId);
                            response = Ok(val);
                        }
                        else
                            throw new Exception();
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptRequest([FromBody]RequestDto requestDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                switch (requestDto.Type)
                {
                    case (int) RequestType.DirectToProject:
                        if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, requestDto.ProjectId))
                        {
                            var val = await _requestRepository.AcceptRequestAsync(requestDto.DeveloperId, requestDto.ProjectId);
                            response = Ok(val);
                        }
                        else
                            throw new Exception();
                        break;
                    case (int) RequestType.DirectToUser:
                        if (user.Role == (int) RoleType.Developer && user.Id == requestDto.DeveloperId)
                        {
                            var val = await _requestRepository.AcceptRequestAsync(user.Id, requestDto.ProjectId);
                            response = Ok(val);
                        }
                        else
                            throw new Exception();
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpPost("fromProject")]
        public async Task<IActionResult> PostRequestFromProject([FromBody]RequestDto requestDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer && await _projectService.CheckOwner(user.Id, requestDto.ProjectId))
                {
                    var val = await _requestRepository.PostRequestFromProjectAsync(requestDto);
                    response = Ok(val);
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
        
        [HttpPost("fromUser")]
        public async Task<IActionResult> PostRequestFromDeveloper([FromBody] RequestDto requestDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int) RoleType.Developer)
                {
                    requestDto.DeveloperId = user.Id;
                    var val = await _requestRepository.PostRequestFromDeveloperAsync(requestDto);
                    response = Ok(val);
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