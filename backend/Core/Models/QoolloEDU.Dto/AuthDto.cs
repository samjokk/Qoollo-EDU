using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class AuthDto
    {
        [JsonProperty("photo")] public string Photo { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("surname")] public string Surname { get; set; }
        [JsonProperty("roleUrl")] public string RoleUrl { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }
}