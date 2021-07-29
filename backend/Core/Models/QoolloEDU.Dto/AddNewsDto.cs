using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class AddNewsDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("content")] public string Content { get; set; }
    }
}