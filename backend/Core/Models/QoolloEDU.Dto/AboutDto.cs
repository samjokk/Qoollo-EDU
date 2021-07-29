using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class AboutDto
    {
        [JsonProperty("newAbout")] public string NewAbout { get; set; }
    }
}