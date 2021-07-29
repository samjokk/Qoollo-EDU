using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class EventRating
    {
        public int DeveloperId { get; set; }
        public int EventId { get; set; }
        public RatingType Rating { get; set; }

        public Developer Developer { get; set; }
        public Event Event { get; set; }
    }
}