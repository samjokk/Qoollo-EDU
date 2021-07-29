using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    
    [JsonObject]
    public class ProjectPageDto
    {
        [JsonProperty("project")] public ProjectDto Project { get; set; }
        [JsonProperty("owner")] public UserMiniDto Owner { get; set; }
        [JsonProperty("event")] public EventMiniDto Event { get; set; }
        [JsonProperty("mediaContent")] public List<MediaContentDto> MediaContent { get; set; }
        [JsonProperty("like")] public int Like { get; set; }
        [JsonProperty("dislike")] public int Dislike { get; set; }
        [JsonProperty("news")] public List<NewsDto> News { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
        [JsonProperty("members")] public List<UserMiniDto> Members { get; set; }
        [JsonProperty("requests")] public List<ProjectRequestDto> Requests { get; set; }
        [JsonProperty("ratingFlag")] public int RatingFlag { get; set; }
        [JsonProperty("memberFlag")] public int MemberFlag { get; set; }
    }
}