using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class ProjectRating
    {
        public int DeveloperId { get; set; }
        public int ProjectId { get; set; }
        public RatingType Rating { get; set; }

        public Developer Developer { get; set; }
        public Project Project { get; set; }
    }
}