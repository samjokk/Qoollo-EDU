using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class PhotoDto
    {
        [JsonProperty("base64")] public string Base64 { get; set; }
    }
}