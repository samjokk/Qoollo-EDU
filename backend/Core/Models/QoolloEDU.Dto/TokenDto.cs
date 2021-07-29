using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class TokenDto
    {
        [JsonProperty("token")] public string Token { get; set; }
    }
}