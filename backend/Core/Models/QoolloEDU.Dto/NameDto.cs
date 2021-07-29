using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class NameDto
    {
        [JsonProperty("newName")] public string NewName { get; set; }
    }
}