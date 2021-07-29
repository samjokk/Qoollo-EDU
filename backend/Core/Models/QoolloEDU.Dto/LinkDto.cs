#nullable enable
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class LinkDto
    {
        [JsonProperty("type")] public int Type { get; set; }
        [JsonProperty("url")] public string? Url { get; set; }
        [JsonProperty("name")] public string? Name { get; set; }
    }
}