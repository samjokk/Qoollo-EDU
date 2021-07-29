using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Database.Repositories.TagRepository;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.RequestRepository
{
    public class RequestRepository: IRequestRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly ITagRepository _tagRepository;

        public RequestRepository(QoolloEduDbContext context,
            ITagRepository tagRepository)
        { 
            _context = context;
            _tagRepository = tagRepository;
        }

        public async Task<List<RequestDto>> GetRequestProjectAsync(int projectId)
        {
            var requests = await _context.Request
                .Where(r => r.ProjectId == projectId)
                .Where(r => r.Type == RequestType.DirectToProject)
                .ToListAsync();
            
            
            return requests.Select(request => new RequestDto()
            {
                DeveloperId = request.DeveloperId,
                ProjectId = request.ProjectId,
                Message = request.Message,
                Type = (int) request.Type
            }).ToList();
        }
        
        public async Task<List<RequestDto>> GetRequestDeveloperAsync(int developerId)
        {
            var requests = await _context.Request
                .Where(r => r.DeveloperId == developerId)
                .Where(r => r.Type == RequestType.DirectToUser)
                .ToListAsync();
            
            
            return requests.Select(request => new RequestDto()
            {
                DeveloperId = request.DeveloperId,
                ProjectId = request.ProjectId,
                Message = request.Message,
                Type = (int) request.Type
            }).ToList();
        }

        public async Task<int> AcceptRequestAsync(int developerId, int projectId)
        {
            var request = await _context.Request
                .Where(r => r.DeveloperId == developerId)
                .Where(r => r.ProjectId == projectId)
                .FirstOrDefaultAsync();
            
            if (request == null)
                throw new Exception($"Request doesn't exist.");

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();
            
            var developerProject = await _context.DeveloperProject
                .Where(dp => dp.DeveloperId == developerId)
                .Where(dp => dp.ProjectId == projectId)
                .FirstOrDefaultAsync();
            
            if (developerProject != null)
                throw new Exception($"Developer already member of this project.");
            
            await _context.DeveloperProject.AddAsync(new DeveloperProject()
            {
                DeveloperId = developerId,
                ProjectId = projectId
            });
            
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> DeclineRequestAsync(int developerId, int projectId)
        {
            var request = await _context.Request
                .Where(r => r.DeveloperId == developerId)
                .Where(r => r.ProjectId == projectId)
                .FirstOrDefaultAsync();
            
            if (request == null)
                throw new Exception($"This developer hasn't send a request to this project.");

            _context.Request.Remove(request);
            return await _context.SaveChangesAsync();
        }
        
        
        public async Task<int> PostRequestFromDeveloperAsync(RequestDto requestDto)
        {
            var requestProject = await _context.Request
                .Where(r => r.DeveloperId == requestDto.DeveloperId)
                .Where(r => r.ProjectId == requestDto.ProjectId)
                .Where(r => r.Type == RequestType.DirectToProject)
                .FirstOrDefaultAsync();
            
            var requestUser = await _context.Request
                .Where(r => r.DeveloperId == requestDto.DeveloperId)
                .Where(r => r.ProjectId == requestDto.ProjectId)
                .Where(r => r.Type == RequestType.DirectToUser)
                .FirstOrDefaultAsync();

            if (requestProject != null)
                await AcceptRequestAsync(requestDto.DeveloperId, requestDto.ProjectId);
            
            if (requestUser != null)
                throw new Exception($"This developer already send a request to this project.");

            await _context.Request.AddAsync(new Request()
            {
                DeveloperId = requestDto.DeveloperId,
                ProjectId = requestDto.ProjectId,
                Message = requestDto.Message,
                Type = (RequestType) requestDto.Type
            });
            
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PostRequestFromProjectAsync(RequestDto requestDto)
        {
            var requestProject = await _context.Request
                .Where(r => r.DeveloperId == requestDto.DeveloperId)
                .Where(r => r.ProjectId == requestDto.ProjectId)
                .Where(r => r.Type == RequestType.DirectToProject)
                .FirstOrDefaultAsync();
            
            var requestUser = await _context.Request
                .Where(r => r.DeveloperId == requestDto.DeveloperId)
                .Where(r => r.ProjectId == requestDto.ProjectId)
                .Where(r => r.Type == RequestType.DirectToUser)
                .FirstOrDefaultAsync();

            if (requestUser != null)
                await AcceptRequestAsync(requestDto.DeveloperId, requestDto.ProjectId);
            
            if (requestProject != null)
                throw new Exception($"This project already send a request to this user.");

            await _context.Request.AddAsync(new Request()
            {
                DeveloperId = requestDto.DeveloperId,
                ProjectId = requestDto.ProjectId,
                Message = requestDto.Message,
                Type = (RequestType) requestDto.Type
            });
            
            return await _context.SaveChangesAsync();
        }
        
        public async Task<List<ProjectRequestDto>> GetRequestProjectUserAsync(int projectId)
        {
            var requests = await _context.Request
                .Where(r => r.ProjectId == projectId)
                .Where(r => r.Type == RequestType.DirectToProject)
                .Include(r => r.Developer)
                .Include(r => r.Developer.BaseUser)
                .ToListAsync();
            
            
            return requests.Select(request => new ProjectRequestDto()
            {
                User = new UserMiniDto()
                {
                    Photo = request.Developer.BaseUser.Photo,
                    Name = request.Developer.Name,
                    Surname = request.Developer.Surname,
                    Nickname = request.Developer.Nickname,
                },
                Request = new RequestDto()
                {
                    DeveloperId = request.DeveloperId,
                    ProjectId = request.ProjectId,
                    Message = request.Message,
                    Type = (int) request.Type
                }
                
            }).ToList();
        }
    }
}