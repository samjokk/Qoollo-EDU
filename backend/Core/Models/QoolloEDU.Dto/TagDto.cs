using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class TagDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("color")] public string Color { get; set; }
    }
}