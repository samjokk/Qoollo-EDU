using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class MediaContent
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public MediaType Type { get; set; }
        public string URL { get; set; }

        public Project Project { get; set; }
    }
}