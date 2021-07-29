using System.Collections.Generic;
using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.models
{
    public class Request
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public int ProjectId { get; set; }
        public string Message { get; set; }
        public RequestType Type { get; set; }
        
        public ICollection<RequestTag>? RequestTags { get; set; }
        public Developer Developer { get; set; }
        public Project Project { get; set; }
    }
}