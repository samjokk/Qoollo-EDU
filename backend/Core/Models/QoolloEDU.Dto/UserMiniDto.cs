using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class UserMiniDto
    {
        [JsonProperty("photo")] public string Photo { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("surname")] public string Surname { get; set; }
    }
}