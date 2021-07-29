using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.MediaContentRepository
{
    public class MediaContentRepository: IMediaContentRepository
    {
        private readonly QoolloEduDbContext _context;
        public MediaContentRepository(QoolloEduDbContext context)
        {
            _context = context;
        }

        public async Task<List<MediaContentDto>> GetMediaContentAsync(int projectId)
        {
            var mediaContent = await _context.MediaContent
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
            return mediaContent.Select(mediacontent => new MediaContentDto() 
                {Link = mediacontent.URL,
                    Type = (int) mediacontent.Type,}).ToList();
        }
        
        public async Task AddMediaContentAsync(int projectId, MediaContentDto mediaContentDto)
        {
            await _context.MediaContent.AddAsync(new MediaContent()
            {
                ProjectId = projectId,
                URL = mediaContentDto.Link,
                Type = (MediaType) mediaContentDto.Type,
            });
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteMediaContentAsync(int projectId, MediaContentDto mediaContentDto)
        {
            var mediaContent = await _context.MediaContent
                .Where(m => m.ProjectId == projectId)
                .Where(m => m.Type == (MediaType) mediaContentDto.Type)
                .Where(m => m.URL == mediaContentDto.Link)
                .FirstOrDefaultAsync();
            
            _context.MediaContent.Remove(mediaContent);
            await _context.SaveChangesAsync();
        }
    }
}
