using System;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProjectDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("markdown")] public string Markdown { get; set; }
        [JsonProperty("creationDate")] public DateTime CreationDate { get; set; }
    }
}