using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class RequestDto
    {
        [JsonProperty("developerId")] public int DeveloperId { get; set; }
        [JsonProperty("projectId")] public int ProjectId { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("type")] public int Type { get; set; }
    }
}