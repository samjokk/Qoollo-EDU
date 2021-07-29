using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.AuthRepository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly QoolloEduDbContext _context;

        public AuthRepository(QoolloEduDbContext context)
        {
            _context = context;
        }
        
        public async Task<AccountDto> CheckLoginAsync(string email, string password)
        {
            var baseUser = await _context.BaseUser.FirstOrDefaultAsync(bu => bu.Email == email);
            if (_checkLogin(baseUser.Salt, baseUser.Hash, password))
            {
                return new AccountDto()
                {
                    Email = email,
                    Password = null,
                };
            }

            return null;
        }
        
        public async Task<RoleDto> GetRoleAsync(string email)
        {
            var baseUser = await _context.BaseUser.FirstOrDefaultAsync(bu => bu.Email == email);
            var dev = await _context.Developer.FirstOrDefaultAsync(d => d.UserId == baseUser.Id);
            if (dev != null)
                return new RoleDto()
                {
                    Role = (int) RoleType.Developer,
                    Id = dev.Id
                };
            var org = await _context.Organizer.FirstOrDefaultAsync(o => o.UserId == baseUser.Id);
            return new RoleDto()
            {
                Role = (int) RoleType.Organizer,
                Id = org.Id
            };
        }

        public async Task<AuthDto> GetUserAsync(string email)
        {
            var baseUser = await _context.BaseUser.FirstOrDefaultAsync(bu => bu.Email == email);
            var dev = await _context.Developer.FirstOrDefaultAsync(d => d.UserId == baseUser.Id);
            if (dev != null)
                return new AuthDto()
                {
                    Photo = baseUser.Photo,
                    Name = dev.Name,
                    Surname = dev.Surname,
                    RoleUrl = "profile",
                    Url = dev.Nickname
                };
            var org = await _context.Organizer.FirstOrDefaultAsync(o => o.UserId == baseUser.Id);
            return new AuthDto()
            {
                Photo = baseUser.Photo,
                Name = org.Name,
                Surname = null,
                RoleUrl = "organizer",
                Url = org.Id.ToString()
            };
        }

        private static bool _checkLogin(string salt, string hash, string password)
        {
            return salt + password == hash;
        }
        
        public async Task<int> GetBaseUserIdAsync(RoleDto roleDto)
        {
            if (roleDto.Role == (int) RoleType.Organizer)
            {
                var org = await _context.Organizer
                    .Where(o => o.Id == roleDto.Id)
                    .FirstOrDefaultAsync();
                return org.UserId;
            }
            else
            {
                var dev = await _context.Developer
                    .Where(d => d.Id == roleDto.Id)
                    .FirstOrDefaultAsync();
                return dev.UserId;
            }
        }

        public async Task<FlagType> GetDeveloperRatingFlagAsync(int developerId, int userId)
        {
            var rat = await _context.DeveloperRating
                .Where(dr => dr.DeveloperIdFrom == userId)
                .Where(dr => dr.DeveloperIdTo == developerId)
                .FirstOrDefaultAsync();
            if (rat != null)
            {
                return rat.Rating == RatingType.Like ? FlagType.SetLike : FlagType.SetDislike;
            }
            else
            {
                var projects = await _context.Project
                    .ToListAsync();
                foreach (var project in projects)
                {
                    var dev1 = await _context.DeveloperProject
                        .Where(dp => dp.ProjectId == project.Id)
                        .Where(dp => dp.DeveloperId == developerId)
                        .FirstOrDefaultAsync();
                    var dev2 = await _context.DeveloperProject
                        .Where(dp => dp.ProjectId == project.Id)
                        .Where(dp => dp.DeveloperId == userId)
                        .FirstOrDefaultAsync();
                    if (dev1 != null && dev2 != null) return FlagType.CanSet;
                }
            }
            return FlagType.CantSet;
        }
    }
}