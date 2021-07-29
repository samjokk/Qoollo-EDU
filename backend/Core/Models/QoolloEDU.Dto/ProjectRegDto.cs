using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProjectRegDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("markdown")] public string Markdown { get; set; }
        [JsonProperty("eventId")] public int? EventId { get; set; }
        [JsonProperty("tags")] public List<TagDto>? Tags { get; set; }
        [JsonProperty("mediaContent")] public List<MediaContentDto>? MediaContent { get; set; }
    }
}