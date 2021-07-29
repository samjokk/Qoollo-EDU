using System.Collections.Generic;

namespace QoolloEDU.Database.models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        
        public ICollection<EventTag> EventTags { get; set; }
        public ICollection<DeveloperTag> DeveloperTags { get; set; }
        public ICollection<RequestTag> RequestTags { get; set; }
        public ICollection<ProjectTag> ProjectTags { get; set; }
    }
}