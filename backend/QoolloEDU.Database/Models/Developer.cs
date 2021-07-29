using System;
using System.Collections.Generic;


namespace QoolloEDU.Database.models
{
    public class Developer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nickname { get; set; }
        
        public BaseUser BaseUser { get; set; }
        public ICollection<DeveloperProject> DeveloperProjects { get; set; }
        public ICollection<DeveloperTag> DeveloperTags { get; set; }
        public ICollection<DeveloperRating> DeveloperRatings { get; set; }
        public ICollection<EventRating> EventRatings { get; set; }
        public ICollection<ProjectRating> ProjectRatings { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}