using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.RatingRepository
{
    public class RatingRepository: IRatingRepository
    {
        private readonly QoolloEduDbContext _context;

        public RatingRepository(QoolloEduDbContext context)
        { 
            _context = context;
        }

        public async Task<int> GetDeveloperRatingAsync(int developerId)
        {
            var like = await _context.DeveloperRating
                .Where(dr => dr.DeveloperIdTo == developerId)
                .Where(dr => dr.Rating == RatingType.Like).ToListAsync();
            
            var dislike = await _context.DeveloperRating
                .Where(dr => dr.DeveloperIdTo == developerId)
                .Where(dr => dr.Rating == RatingType.Dislike).ToListAsync();
            
            return like.Count - dislike.Count;
        }

        public async Task<int> GetProjectLikeAsync(int projectId)
        {
            var like = await _context.ProjectRating
                .Where(pr => pr.ProjectId == projectId)
                .Where(pr => pr.Rating == RatingType.Like).ToListAsync();
            return like.Count;
        }

        public async Task<int> GetProjectDislikeAsync(int projectId)
        {
            var dislike = await _context.ProjectRating
                .Where(pr => pr.ProjectId == projectId)
                .Where(pr => pr.Rating == RatingType.Dislike).ToListAsync();
            return dislike.Count;
        }

        public async Task<int> GetEventLikeAsync(int eventId)
        {
            var like = await _context.EventRating
                .Where(er => er.EventId == eventId)
                .Where(er => er.Rating == RatingType.Like).ToListAsync();
            return like.Count;
        }

        public async Task<int> GetEventDislikeAsync(int eventId)
        {
            var dislike = await _context.EventRating
                .Where(er => er.EventId == eventId)
                .Where(er => er.Rating == RatingType.Dislike).ToListAsync();
            return dislike.Count;
        }
        
        public async Task<int> PutDeveloperRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating)
        {
            var developerRating = await _context.DeveloperRating
                .Where(dr => dr.DeveloperIdFrom == developerIdFrom)
                .Where(dr => dr.DeveloperIdTo == developerIdTo).FirstOrDefaultAsync();

            if (developerRating == null)
                throw new Exception($"Developer hasn't set rating for this developer.");
            
            developerRating.Rating = rating;
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> PutProjectRatingAsync(int developerId, int projectId, RatingType rating)
        {
            var projectRating = await _context.ProjectRating
                .Where(pr => pr.DeveloperId == developerId)
                .Where(pr => pr.ProjectId == projectId).FirstOrDefaultAsync();

            if (projectRating == null)
                throw new Exception($"Developer hasn't set rating for this project.");
            
            projectRating.Rating = rating;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> PutEventRatingAsync(int developerId, int eventId, RatingType rating)
        {
            var eventRating = await _context.EventRating
                .Where(er => er.DeveloperId == developerId)
                .Where(er => er.EventId == eventId).FirstOrDefaultAsync();

            if (eventRating == null)
                throw new Exception($"Developer hasn't set rating for this event.");
            
            eventRating.Rating = rating;
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> PostDeveloperRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating)
        {
            var developerRating = await _context.DeveloperRating
                .Where(dr => dr.DeveloperIdFrom == developerIdFrom)
                .Where(dr => dr.DeveloperIdTo == developerIdTo).FirstOrDefaultAsync();
            
            if (developerRating != null)
                throw new Exception($"Developer already have rating from this developer.");
            
            await _context.DeveloperRating.AddAsync(new DeveloperRating()
            {
                DeveloperIdFrom = developerIdFrom,
                DeveloperIdTo = developerIdTo,
                Rating = rating
            });
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> PostProjectRatingAsync(int developerId, int projectId, RatingType rating)
        {
            var projectRating = await _context.ProjectRating
                .Where(pr => pr.DeveloperId == developerId)
                .Where(pr => pr.ProjectId == projectId).FirstOrDefaultAsync();
            
            if (projectRating != null)
                throw new Exception($"Project already have rating from this developer.");
            
            await _context.ProjectRating.AddAsync(new ProjectRating()
            {
                DeveloperId = developerId,
                ProjectId = projectId,
                Rating = rating
            });
            return await _context.SaveChangesAsync();
        }

        public async Task<int>  PostEventRatingAsync(int developerId, int eventId, RatingType rating)
        {
            var eventRating = await _context.EventRating
                .Where(er => er.DeveloperId == developerId)
                .Where(er => er.EventId == eventId).FirstOrDefaultAsync();
            
            if (eventRating != null)
                throw new Exception($"Event already have rating from this developer.");
            
            await _context.EventRating.AddAsync(new EventRating()
            {
                DeveloperId = developerId,
                EventId = eventId,
                Rating = rating
            });
            return await _context.SaveChangesAsync();
        }


        public async Task<int> DeleteDeveloperRatingAsync(int developerIdFrom, int developerIdTo)
        {
            var developerRating = await _context.DeveloperRating
                .Where(dr => dr.DeveloperIdFrom == developerIdFrom)
                .Where(dr => dr.DeveloperIdTo == developerIdTo).FirstOrDefaultAsync();
            
            if (developerRating == null)
                throw new Exception($"Developer hasn't set rating for this developer.");

            _context.DeveloperRating.Remove(developerRating);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProjectRatingAsync(int developerId, int projectId)
        {
            var projectRating = await _context.ProjectRating
                .Where(pr => pr.DeveloperId == developerId)
                .Where(pr => pr.ProjectId == projectId).FirstOrDefaultAsync();
            
            if (projectRating == null)
                throw new Exception($"Developer hasn't set rating for this project.");
            
            _context.ProjectRating.Remove(projectRating);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEventRatingAsync(int developerId, int eventId)
        {
            var eventRating = await _context.EventRating
                .Where(er => er.DeveloperId == developerId)
                .Where(er => er.EventId == eventId).FirstOrDefaultAsync();
            
            if (eventRating == null)
                throw new Exception($"Developer hasn't set rating for this event.");
            
            _context.EventRating.Remove(eventRating);
            return await _context.SaveChangesAsync();
        }
    }
}