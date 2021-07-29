using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProfileRequestDto
    {
        [JsonProperty("base64")] public string Base64 { get; set; }
    }
}