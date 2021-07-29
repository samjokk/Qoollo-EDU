using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.ProfilePageRepository;
using QoolloEDU.Dto;
using QoolloEDU.WebService.Services;


namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfilePageRepository _profilePageRepository;
        private readonly AuthService _authService;
        public ProfileController(IProfilePageRepository profilePageRepository,
            AuthService authService)
        {
            _profilePageRepository = profilePageRepository;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            IActionResult response;
            try
            {
                var profiles = await _profilePageRepository.GetAllProfileAsync();
                response = Ok(profiles);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpGet("{nickname}")]
        public async Task<IActionResult> GetProfile(string nickname)
        {
            IActionResult response;
            try
            {
                var developerId = await _profilePageRepository.GetDeveloperIdByNicknameAsync(nickname);
                var profilePage = await _profilePageRepository.GetProfileAsync(developerId);

                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' });
                if (token.Length > 1)
                {
                    var user = await _authService.DecodeRoleAsync(token[1]);
                    if (user.Role == (int)RoleType.Developer)
                        profilePage.RatingFlag = (int)await _authService.GetDeveloperRatingFlagAsync(developerId, user.Id);
                    else
                    {
                        profilePage.Requests = null;
                        profilePage.RatingFlag = (int)FlagType.CantSet;
                    }
                }
                else
                {
                    profilePage.Requests = null;
                    profilePage.RatingFlag = (int)FlagType.CantSet;
                }

                response = Ok(profilePage);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPut("about")]
        public async Task<IActionResult> UpdateAbout([FromBody] AboutDto aboutDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer)
                    await _profilePageRepository.UpdateAboutAsync(user.Id, aboutDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPost("tag")]
        public async Task<IActionResult> AddTag([FromBody] TagDto tagDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer)
                    await _profilePageRepository.AddTagAsync(user.Id, tagDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpDelete("tag")]
        public async Task<IActionResult> DeleteTag([FromBody] TagDto tagDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer)
                    await _profilePageRepository.DeleteTagAsync(user.Id, tagDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPost("rating/{developerIdTo:int}")]
        public async Task<IActionResult> PostRating(int developerIdTo, [FromBody] RatingDto ratingDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer &&
                    await _authService.GetDeveloperRatingFlagAsync(developerIdTo, user.Id) == FlagType.CanSet)
                    await _profilePageRepository.PostRatingAsync(user.Id, developerIdTo, (RatingType)ratingDto.Rating);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPut("rating/{developerIdTo:int}")]
        public async Task<IActionResult> PutRating(int developerIdTo, [FromBody] RatingDto ratingDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer &&
                    (await _authService.GetDeveloperRatingFlagAsync(developerIdTo, user.Id) == FlagType.SetLike ||
                     await _authService.GetDeveloperRatingFlagAsync(developerIdTo, user.Id) == FlagType.SetDislike))
                    await _profilePageRepository.PutRatingAsync(user.Id, developerIdTo, (RatingType)ratingDto.Rating);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpDelete("rating/{developerIdTo:int}")]
        public async Task<IActionResult> DeleteRating(int developerIdTo)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer &&
                    (await _authService.GetDeveloperRatingFlagAsync(developerIdTo, user.Id) == FlagType.SetLike ||
                     await _authService.GetDeveloperRatingFlagAsync(developerIdTo, user.Id) == FlagType.SetDislike))
                    await _profilePageRepository.DeleteRatingAsync(user.Id, developerIdTo);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPost("link")]
        public async Task<IActionResult> AddLink([FromBody] LinkDto linkDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer)
                    await _profilePageRepository.AddLinkAsync(await _authService.GetBaseUserId(user), linkDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpDelete("link")]
        public async Task<IActionResult> DeleteLink([FromBody] LinkDto linkDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer)
                    await _profilePageRepository.DeleteLinkAsync(await _authService.GetBaseUserId(user), linkDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserRegDto userRegDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' });
                if (token.Length <= 1)
                    response = Ok(await _profilePageRepository.CreateAsync(userRegDto));
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        [HttpPut("photo")]
        public async Task<IActionResult> PutPhoto([FromBody] PhotoDto photoDto)
        {
            IActionResult response;
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(new char[] { ' ' })[1];
                var user = await _authService.DecodeRoleAsync(token);
                if (user.Role == (int)RoleType.Developer)
                    await _profilePageRepository.PutPhotoAsync(user.Id, photoDto);
                else
                    throw new Exception();
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }
            return response;
        }
    }
}