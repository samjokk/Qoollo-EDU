using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class RegistrationDto
    {
        [JsonProperty("errorCode")] public int ErrorCode { get; set; }
        [JsonProperty("url")] public string? Url { get; set; }
    }
}