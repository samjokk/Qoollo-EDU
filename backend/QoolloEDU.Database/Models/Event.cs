using System;
using System.Collections.Generic;

namespace QoolloEDU.Database.models
{
    public class Event
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public ICollection<EventTag> EventTags { get; set; }
        public ICollection<EventRating> EventRatings { get; set; }
        public ICollection<Project> Projects { get; set; }

        public Organizer Organizer { get; set; }
    }
}