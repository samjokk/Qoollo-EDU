using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProjectCardMiniDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("organizerPhoto")] public string OrganizerPhoto { get; set; }
        [JsonProperty("rating")] public int Rating { get; set; }
        
    }
}