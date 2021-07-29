using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class RoleDto
    {
        [JsonProperty("role")] public int Role { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
    }
}