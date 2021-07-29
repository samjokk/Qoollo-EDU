#nullable enable
using System;
using System.Collections.Generic;
using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class Project
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? EventId { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Markdown { get; set; }
        public PlaceType? Place { get; set; }

        public ICollection<DeveloperProject> DeveloperProjects { get; set; }
        public ICollection<ProjectTag> ProjectTags { get; set; }

        public ICollection<News> News { get; set; }
        public ICollection<MediaContent> MediaContents { get; set; }
        public ICollection<Request> Requests { get; set; }

        public ICollection<ProjectRating> ProjectRatings { get; set; }
        

        public Event? Event { get; set; }
    }
}