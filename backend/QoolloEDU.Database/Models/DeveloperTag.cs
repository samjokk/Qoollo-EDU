namespace QoolloEDU.Database.models
{
    public class DeveloperTag
    {
        public int DeveloperId { get; set; }
        public int TagId { get; set; }
        
        public Developer Developer { get; set; }
        public Tag Tag { get; set; }
    }
}