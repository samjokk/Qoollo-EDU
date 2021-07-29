using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class SearchProjectPageDto
    {
        [JsonProperty("topProjects")] public List<ProjectCardMiniDto> TopProjects { get; set; }
        [JsonProperty("projects")] public List<ProjectCardDto> Projects { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
    }
}