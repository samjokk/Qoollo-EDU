using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class EventRegDto
    {
        [JsonProperty("organizerId")] public int OrganizerId { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("markdown")] public string Markdown { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
        
        [JsonProperty("startDate")] public DateTime StartDate { get; set; }
        [JsonProperty("endDate")] public DateTime EndDate { get; set; }
    }
}