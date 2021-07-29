using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QoolloEDU.Database.Repositories.AuthRepository;
using QoolloEDU.Dto;
using QoolloEDU.WebService.Services;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly AuthService _authService;
        
        public AuthController(IAuthRepository authRepository, AuthService authService)
        {
            _authRepository = authRepository;
            _authService = authService;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] AccountDto accountDto)
        {
            IActionResult response;
            try
            {
                var identity = await GetIdentity(accountDto.Email, accountDto.Password);
                
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid email or password." });
                }
               
                var now = DateTime.UtcNow;
                // create json web token
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                jwt.Payload["role"] = await _authService.EncodeRoleAsync(identity.Name);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                
                var accountResponse = new
                {
                    access_token = encodedJwt,
                    user = await _authService.GetUserAsync(identity.Name)
                };

                response = Ok(accountResponse);
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var account = await _authRepository.CheckLoginAsync(email, password);
            if (account == null) return null;
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Email)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}