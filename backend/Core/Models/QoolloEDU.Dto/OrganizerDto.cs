using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class OrganizerDto
    {
        [JsonProperty("photo")] public string Photo { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("about")] public string About { get; set; }
        [JsonProperty("projectCount")] public int ProjectCount { get; set; }
        [JsonProperty("eventCount")] public int EventCount { get; set; }
    }
}