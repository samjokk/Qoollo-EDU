using System.Collections.Generic;
using System.Threading.Tasks;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.Repositories.RatingRepository
{
    public interface IRatingRepository
    {
        Task<int> GetDeveloperRatingAsync(int developerId);
        
        Task<int> GetProjectLikeAsync(int projectId);
        Task<int> GetProjectDislikeAsync(int projectId);
        
        Task<int> GetEventLikeAsync(int eventId);
        Task<int> GetEventDislikeAsync(int eventId);

        Task<int> PutDeveloperRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating);
        Task<int> PutProjectRatingAsync(int developerId, int projectId, RatingType rating);
        Task<int> PutEventRatingAsync(int developerId, int eventId, RatingType rating);
        
        Task<int> PostDeveloperRatingAsync(int developerIdFrom, int developerIdTo, RatingType rating);
        Task<int> PostProjectRatingAsync(int developerId, int projectId, RatingType rating);
        Task<int> PostEventRatingAsync(int developerId, int eventId, RatingType rating);
        
        Task<int> DeleteDeveloperRatingAsync(int developerIdFrom, int developerIdTo);
        Task<int> DeleteProjectRatingAsync(int developerId, int projectId);
        Task<int> DeleteEventRatingAsync(int developerId, int eventId);
    }
}