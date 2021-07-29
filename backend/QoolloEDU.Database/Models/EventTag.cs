namespace QoolloEDU.Database.models
{
    public class EventTag
    {
        public int TagId { get; set; }
        public int EventId { get; set; }
        
        public Tag Tag { get; set; }
        public Event Event { get; set; }
    }
}