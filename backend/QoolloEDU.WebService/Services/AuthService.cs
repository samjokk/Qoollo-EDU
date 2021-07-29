using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.AuthRepository;
using QoolloEDU.Database.Repositories.DeveloperRepository;
using QoolloEDU.Dto;

namespace QoolloEDU.WebService.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<RoleDto> EncodeRoleAsync(string email)
        {
            return await _authRepository.GetRoleAsync(email);
        }
        
        public async Task<RoleDto> DecodeRoleAsync(string token)
        {
            const string secret = AuthOptions.KEY;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            // ReSharper disable once PossibleNullReferenceException
            return await _authRepository.GetRoleAsync(claims.Identity.Name);
        }

        public async Task<AuthDto> GetUserAsync(string email)
        {
            return await _authRepository.GetUserAsync(email);
        }
        
        public async Task<int> GetBaseUserId(RoleDto roleDto)
        {
            return await _authRepository.GetBaseUserIdAsync(roleDto);
        }

        private static string GetName(string token)
        {
            const string secret = AuthOptions.KEY;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            // ReSharper disable once PossibleNullReferenceException
            return claims.Identity.Name;
        }
        
        public async Task<FlagType> GetDeveloperRatingFlagAsync(int developerId, int userId)
        {
            return await _authRepository.GetDeveloperRatingFlagAsync(developerId, userId);
        }
        
        
    }
}