using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.Repositories.DeveloperRepository;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.NewsRepository
{
    public class NewsRepository : INewsRepository
    {
        private readonly QoolloEduDbContext _context;
        private readonly IDeveloperRepository _developerRepository;
        public NewsRepository(QoolloEduDbContext context,
            IDeveloperRepository developerRepository)
        {
            _context = context;
            _developerRepository = developerRepository;
        }
        
        public async Task<List<NewsDto>> GetNewsAsync(int projectId)
        {
            var news = await _context.News
                .Where(n => n.ProjectId == projectId)
                .ToListAsync();
            
            return news.Select(news => new NewsDto()
                {Id = news.Id, Name = news.Name, Content = news.Description, PublishDate = news.Date}).ToList();
        }
        
        public async Task<int> PostNewsAsync(int projectId, AddNewsDto addNewsDto)
        {
            var news = await _context.News.AddAsync(new News()
            {
                ProjectId = projectId,
                Name = addNewsDto.Name,
                Description = addNewsDto.Content,
                Date = DateTime.Now
            });
            await _context.SaveChangesAsync();
            return news.Entity.Id;
        }
        
        public async Task<NewsPageDto> GetNewsPageAsync(int projectId, int newsId)
        {
            var news = await _context.News
                .Where(n => n.ProjectId == projectId)
                .OrderBy(n => n.Date)
                .ToListAsync();
            var project = await _context.Project
                .Where(p => p.Id == projectId)
                .FirstOrDefaultAsync();
            var prev = new NewsDto();
            var cur = new NewsDto();
            var next = new NewsDto();

            if (news.Count == 1)
            {
                cur = new NewsDto()
                {
                    Id = news[0].Id,
                    Name = news[0].Name,
                    Content = news[0].Description,
                    PublishDate = news[0].Date
                };
            }
            else if (news.Count == 2)
            {
                if (news[0].Id == newsId)
                {
                    cur = new NewsDto()
                    {
                        Id = news[0].Id,
                        Name = news[0].Name,
                        Content = news[0].Description,
                        PublishDate = news[0].Date
                    };
                
                    next = new NewsDto()
                    {
                        Id = news[1].Id,
                        Name = news[1].Name,
                        Content = news[1].Description,
                        PublishDate = news[1].Date
                    };
                }
                else
                {
                    prev = new NewsDto()
                    {
                        Id = news[0].Id,
                        Name = news[0].Name,
                        Content = news[0].Description,
                        PublishDate = news[0].Date
                    };
                
                    cur = new NewsDto()
                    {
                        Id = news[1].Id,
                        Name = news[1].Name,
                        Content = news[1].Description,
                        PublishDate = news[1].Date
                    };
                }
                
            }
            else
            {
                var flag = 0;
                foreach (var news_el in news)
                {
                    if (flag == 1)
                    {
                        next = new NewsDto()
                        {
                            Id = news_el.Id,
                            Name = news_el.Name,
                            Content = news_el.Description,
                            PublishDate = news_el.Date
                        };
                        break;
                    }
                    else
                    {
                        if (news_el.Id == newsId)
                        {
                            flag = 1;
                            cur = new NewsDto()
                            {
                                Id = news_el.Id,
                                Name = news_el.Name,
                                Content = news_el.Description,
                                PublishDate = news_el.Date
                            };
                        }
                        else
                        {
                            prev = new NewsDto()
                            {
                                Id = news_el.Id,
                                Name = news_el.Name,
                                Content = news_el.Description,
                                PublishDate = news_el.Date
                            };
                        }
                    }
                }
                
                if (flag == 0)
                {
                    prev = new NewsDto();
                }
            }
            
            return new NewsPageDto()
            {
                Id = projectId,
                Name = project.Name,
                PrevNews = prev,
                CurNews = cur,
                NextNews = next,
                Comments = await GetCommentsByNewsId(newsId)
            };
        }

        private async Task<List<CommentDto>> GetCommentsByNewsId(int newsId)
        {
            var comments = await _context.Comment
                .Where(c => c.NewsId == newsId)
                .ToListAsync();

            var result = new List<CommentDto>();
            foreach (var comment in comments)
            {
                result.Add(new CommentDto()
                {
                    User = await _developerRepository.GetUserAsync(comment.DeveloperId),
                    Comment = comment.Content
                });
            }

            return result;
        }
        
        public async Task<int> PostCommentAsync(int newsId, int developerId, CommentDto commentDto)
        {
            await _context.Comment.AddAsync(new Comment()
            {
                NewsId = newsId,
                DeveloperId = developerId,
                Content = commentDto.Comment,
                Date = DateTime.Now
            });
            return await _context.SaveChangesAsync();
        }
    }
}