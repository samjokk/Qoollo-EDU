using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class ProfilePageDto
    {
        [JsonProperty("developer")] public UserDto Developer { get; set; }
        [JsonProperty("rating")] public int Rating { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
        [JsonProperty("usefulLinks")] public List<LinkDto> UsefulLinks { get; set; }
        [JsonProperty("contactLinks")] public List<LinkDto> ContactLinks  { get; set; }
        [JsonProperty("projectsEvent")] public List<ProjectCardDto> ProjectsEvent  { get; set; }
        [JsonProperty("projectsNoEvent")] public List<ProjectCardDto> ProjectsNoEvent { get; set; }
        [JsonProperty("achievements")] public List<AchievementDto> Achievements  { get; set; }
        [JsonProperty("requests")] public List<RequestDto> Requests { get; set; }
        [JsonProperty("ratingFlag")] public int RatingFlag { get; set; }
    }
}