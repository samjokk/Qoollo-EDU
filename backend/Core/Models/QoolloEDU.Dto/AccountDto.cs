using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class AccountDto
    {
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }
}