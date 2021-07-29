using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class EventMiniDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}