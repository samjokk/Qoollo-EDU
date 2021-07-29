using System;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class UserRegDto
    {
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("surname")] public string Surname { get; set; }
        [JsonProperty("age")] public DateTime BirthDate { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }
}