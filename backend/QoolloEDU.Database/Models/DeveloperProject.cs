namespace QoolloEDU.Database.models
{
    public class DeveloperProject
    {
        public int DeveloperId { get; set; }
        public int ProjectId { get; set; }
        
        public Developer Developer { get; set; }
        public Project Project { get; set; }
    }
}