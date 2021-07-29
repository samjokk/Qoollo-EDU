using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class UserCardDto
    {
        [JsonProperty("photo")] public string Photo { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("surname")] public string Surname { get; set; }
        [JsonProperty("age")] public int Age { get; set; }
        [JsonProperty("rating")] public int Rating { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
    }
}