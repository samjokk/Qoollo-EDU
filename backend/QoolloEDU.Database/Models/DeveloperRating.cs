using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class DeveloperRating
    {
        public int DeveloperIdFrom { get; set; }
        public int DeveloperIdTo { get; set; }
        public RatingType Rating { get; set; }

        public Developer Developer { get; set; }
    }
}