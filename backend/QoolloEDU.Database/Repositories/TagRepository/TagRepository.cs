using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.TagRepository
{
    public class TagRepository: ITagRepository
    {
        private readonly QoolloEduDbContext _context;

        public TagRepository(QoolloEduDbContext context)
        {
            _context = context;
        }
        public async Task<List<TagDto>> GetAllTagsAsync()
        {
            var tags = await _context.Tag.OrderBy(t => t.Id).ToListAsync();
            return tags.Select(tag => new TagDto() {Id = tag.Id, Name = tag.Name, Color = tag.Color}).ToList();
        }

        public async Task<List<TagDto>> GetDeveloperTagsAsync(int developerId)
        {
            var tags = await _context.DeveloperTag
                .Include(dt => dt.Tag)
                .Where(dt => dt.DeveloperId == developerId)
                .ToListAsync();

            return tags.Select(tag => new TagDto() {Id = tag.Tag.Id, Name = tag.Tag.Name, Color = tag.Tag.Color}).ToList();
        }
        
        public async Task<List<TagDto>> GetProjectTagsAsync(int projectId)
        {
            var tags = await _context.ProjectTag
                .Include(pt => pt.Tag)
                .Where(pt => pt.ProjectId == projectId)
                .ToListAsync();

            return tags.Select(tag => new TagDto() {Id = tag.Tag.Id, Name = tag.Tag.Name, Color = tag.Tag.Color}).ToList();
        }
        
        public async Task<List<TagDto>> GetEventTagsAsync(int eventId)
        {
            var tags = await _context.EventTag
                .Include(et => et.Tag)
                .Where(et => et.EventId == eventId)
                .ToListAsync();

            return tags.Select(tag => new TagDto() {Id = tag.Tag.Id, Name = tag.Tag.Name, Color = tag.Tag.Color}).ToList();
        }
        
        public async Task<List<TagDto>> GetRequestTagsAsync(int requestId)
        {
            var tags = await _context.RequestTag
                .Include(rt => rt.Tag)
                .Where(rt => rt.RequestId == requestId)
                .ToListAsync();

            return tags.Select(tag => new TagDto() {Id = tag.Tag.Id, Name = tag.Tag.Name, Color = tag.Tag.Color}).ToList();
        }
        
        public async Task AddTagDeveloperAsync(int developerId, TagDto tagDto)
        {
            var tag = await _context.DeveloperTag
                .Include(dt => dt.Tag)
                .Where(dt => dt.DeveloperId == developerId)
                .Where(dt => dt.Tag.Id == tagDto.Id)
                .FirstOrDefaultAsync();
            if (tag != null)
                throw new Exception($"Developer already have Tag with such name = {tagDto.Name}.");
            var newTag = await _context.Tag.Where(t => t.Id == tagDto.Id).FirstOrDefaultAsync();
            await _context.DeveloperTag.AddAsync(new DeveloperTag()
            {
                DeveloperId = developerId,
                TagId = newTag.Id
            });
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteTagDeveloperAsync(int developerId, TagDto tagDto)
        {
            var tag = await _context.DeveloperTag
                .Include(dt => dt.Tag)
                .Where(dt => dt.DeveloperId == developerId)
                .Where(dt => dt.Tag.Id == tagDto.Id)
                .FirstOrDefaultAsync();
            if (tag == null)
                throw new Exception($"No Tag with such name = {tagDto.Name}.");

            _context.DeveloperTag.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddProjectTagAsync(int projectId, TagDto tagDto)
        {
            var tag = await _context.ProjectTag
                .Include(pt => pt.Tag)
                .Where(pt => pt.ProjectId == projectId)
                .Where(pt => pt.Tag.Id == tagDto.Id)
                .FirstOrDefaultAsync();
            if (tag != null)
                throw new Exception($"Project already have Tag with such name = {tagDto.Name}.");
            var newTag = await _context.Tag.Where(t => t.Id == tagDto.Id).FirstOrDefaultAsync();
            await _context.ProjectTag.AddAsync(new ProjectTag()
            {
                ProjectId = projectId,
                TagId = newTag.Id
            });
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> AddEventTagAsync(int eventId, TagDto tagDto)
        {
            var tag = await _context.EventTag
                .Where(et => et.EventId == eventId)
                .Where(et => et.TagId == tagDto.Id)
                .FirstOrDefaultAsync();
            if (tag != null)
                throw new Exception($"Event already have Tag with such name = {tagDto.Name}.");
            var newTag = await _context.Tag.Where(t => t.Id == tagDto.Id).FirstOrDefaultAsync();
            await _context.EventTag.AddAsync(new EventTag()
            {
                EventId = eventId,
                TagId = newTag.Id
            });
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> DeleteProjectTagAsync(int projectId, TagDto tagDto)
        {
            var tag = await _context.ProjectTag
                .Where(pt => pt.ProjectId == projectId)
                .Where(pt => pt.TagId == tagDto.Id)
                .FirstOrDefaultAsync();
            if (tag == null)
                throw new Exception($"No Tag with such name = {tagDto.Name}.");

            _context.ProjectTag.Remove(tag);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> DeleteEventTagAsync(int eventId, TagDto tagDto)
        {
            var tag = await _context.EventTag
                .Where(et => et.EventId == eventId)
                .Where(et => et.TagId == tagDto.Id)
                .FirstOrDefaultAsync();
            if (tag == null)
                throw new Exception($"No Tag with such name = {tagDto.Name}.");

            _context.EventTag.Remove(tag);
            return await _context.SaveChangesAsync();
        }
    }
}