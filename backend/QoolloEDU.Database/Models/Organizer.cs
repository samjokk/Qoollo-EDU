using System.Collections.Generic;

namespace QoolloEDU.Database.models
{
    public class Organizer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public BaseUser BaseUser { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}