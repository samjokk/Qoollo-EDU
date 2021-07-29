using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class Link
    {
        public int Id { get; set; }
        public int BaseUserId { get; set; }
        
        public LinkType Type { get; set; }
        public string? URL { get; set; }
        public string? Name { get; set; }

        public BaseUser BaseUser { get; set; }
    }
}