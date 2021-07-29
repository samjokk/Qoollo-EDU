using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.LinkRepository
{
    public class LinkRepository: ILinkRepository
    {
        private readonly QoolloEduDbContext _context;
        public LinkRepository(QoolloEduDbContext context)
        {
            _context = context;
        }

        public async Task<List<LinkDto>> GetUsefulLinksAsync(int baseUserId)
        {
            var links = await _context.Link
                .Include(l => l.BaseUser)
                .Where(l => l.Type == LinkType.Useful)
                .Where(l => l.BaseUser.Id == baseUserId)
                .ToListAsync();
            return links.Select(link => new LinkDto() {Url = link.URL, Type = (int) link.Type, Name = link.Name}).ToList();
        }

        public async Task<List<LinkDto>> GetContactLinksAsync(int baseUserId)
        {
            var links = await _context.Link
                .Include(l => l.BaseUser)
                .Where(l => l.Type == LinkType.Contact)
                .Where(l => l.BaseUser.Id == baseUserId)
                .ToListAsync();
            return links.Select(link => new LinkDto() {Url = link.URL, Type = (int) link.Type, Name = link.Name}).ToList();
        }
        
        public async Task AddLinkAsync(int baseUserId, LinkDto linkDto)
        {
            List<LinkDto> lst;
            int limit;
            if (linkDto.Type == (int) LinkType.Contact)
            {
                lst = await GetContactLinksAsync(baseUserId);
                limit = 5;
            }
            else
            {
                lst = await GetUsefulLinksAsync(baseUserId);
                limit = 10;
            }
            var cnt = lst.Count;
            if (cnt >= limit)
                throw new Exception($"Developer have maximum links of this type.");
            if (linkDto.Name != null)
                await _context.Link.AddAsync(new Link()
                {
                    BaseUserId = baseUserId,
                    URL = linkDto.Url,
                    Type = (LinkType) linkDto.Type,
                    Name = linkDto.Name
                });
            else
            {
                await _context.Link.AddAsync(new Link()
                {
                    BaseUserId = baseUserId,
                    URL = linkDto.Url,
                    Type = (LinkType) linkDto.Type
                });
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLinkAsync(int baseUserId, LinkDto linkDto)
        {
            var link = await _context.Link
                .Where(l => l.BaseUserId == baseUserId)
                .Where(l => l.Type == (LinkType) linkDto.Type)
                .Where(l => l.URL == linkDto.Url)
                .FirstOrDefaultAsync();
            if (link == null)
                throw new Exception($"No Link with such URL = {linkDto.Url}.");

            _context.Link.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}