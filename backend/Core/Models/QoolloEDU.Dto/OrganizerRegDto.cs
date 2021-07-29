﻿using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class OrganizerRegDto
    {
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }
}