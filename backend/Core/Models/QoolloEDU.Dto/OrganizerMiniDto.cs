using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class OrganizerMiniDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("photo")] public string Photo { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}