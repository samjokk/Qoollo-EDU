using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProjectMiniDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("place")] public int Place { get; set; }
    }
}