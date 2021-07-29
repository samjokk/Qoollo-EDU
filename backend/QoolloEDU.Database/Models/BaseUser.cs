using System.Collections.Generic;

#nullable enable
namespace QoolloEDU.Database.models
{
    public class BaseUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string? About { get; set; }
        public string? Photo { get; set; }
        
        public Developer? Developer { get; set; }
        public Organizer? Organizer { get; set; }
        public ICollection<Link> Links { get; set; }
    }
}