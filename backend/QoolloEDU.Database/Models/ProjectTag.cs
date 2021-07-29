namespace QoolloEDU.Database.models
{
    public class ProjectTag
    {
        public int TagId { get; set; }
        public int ProjectId { get; set; }
        
        public Tag Tag { get; set; }
        public Project Project { get; set; }
    }
}