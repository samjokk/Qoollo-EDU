using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.OrganizerRepository
{
    public class OrganizerRepository: IOrganizerRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly string _basePhoto;
        private static readonly Random _random = new Random();
        
        public OrganizerRepository(QoolloEduDbContext context)
        {
            _context = context;
            _basePhoto = "data:image/png;base64,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";
        }

        public async Task<OrganizerDto> GetOrganizerAsync(int orgId)
        {
            var org = await _context.Organizer
                .Where(o => o.Id == orgId)
                .Include(o => o.BaseUser)
                .FirstOrDefaultAsync();
            var eEvent = await _context.Event
                .Where(e => e.OrganizerId == orgId)
                .ToListAsync();
            var proj = await _context.Project
                .Where(p => p.EventId != null)
                .Include(p => p.Event)
                .Where(p => p.Event.OrganizerId == orgId)
                .ToListAsync();
            return new OrganizerDto()
            {
                Name = org.Name,
                Description = org.Description,
                About = org.BaseUser.About,
                Photo = org.BaseUser.Photo,
                EventCount = eEvent.Count,
                ProjectCount = proj.Count
            };
        }

        public async Task<RegistrationDto> CreateOrganizerAsync(OrganizerRegDto organizerRegDto)
        {
            var emailCheck = await _context.BaseUser
                .Where(bu => bu.Email == organizerRegDto.Email)
                .FirstOrDefaultAsync();
            if (emailCheck != null) return new RegistrationDto()
            {
                ErrorCode = (int) CreateUserErrorType.EmailNotUnique,
                Url = null
            };
            
            var nameCheck = await _context.Organizer
                .Where(o => o.Name == organizerRegDto.Name)
                .FirstOrDefaultAsync();
            if (nameCheck != null) return new RegistrationDto()
            {
                ErrorCode = (int) CreateUserErrorType.NicknameNotUnique,
                Url = null
            };
            
            var salt = RandomString(8);
            var bu = await _context.AddAsync(new BaseUser()
            {
                Email = organizerRegDto.Email,
                Salt = salt,
                Hash = salt + organizerRegDto.Password,
                Photo = _basePhoto,
            });
            await _context.SaveChangesAsync();
            var org = await _context.AddAsync(new Organizer()
            {
                UserId = bu.Entity.Id,
                Name = organizerRegDto.Name,
            });
            await _context.SaveChangesAsync();
            return new RegistrationDto()
            {
                ErrorCode = (int) CreateUserErrorType.Ok,
                Url = org.Entity.Id.ToString()
            };
        }
        
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public async Task PutPhotoAsync(int developerId, PhotoDto photoDto)
        {
            var developer = await _context.Developer
                .Include(d => d.BaseUser)
                .FirstOrDefaultAsync(d => d.Id == developerId);
            
            developer.BaseUser.Photo = photoDto.Base64;
            await _context.SaveChangesAsync();
        }
    }
}