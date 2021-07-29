using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class MediaContentDto
    {
        [JsonProperty("link")] public string Link { get; set; }
        [JsonProperty("type")] public int Type { get; set; }
    }
}