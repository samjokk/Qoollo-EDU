using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProjectRequestDto
    {
        [JsonProperty("user")] public UserMiniDto User { get; set; }
        [JsonProperty("request")] public RequestDto Request { get; set; }
    }
}