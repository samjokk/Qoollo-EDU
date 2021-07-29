using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class UserDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("surname")] public string Surname { get; set; }
        [JsonProperty("age")] public int Age { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
        [JsonProperty("about")] public string About { get; set; }
        [JsonProperty("photo")] public string Photo { get; set; }
    }
}