using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class EventPageDto
    {
        [JsonProperty("organizer")] public OrganizerMiniDto Organizer { get; set; }
        [JsonProperty("event")] public EventDto Event { get; set; }
        
        [JsonProperty("projectNum")] public int ProjectNum { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
        [JsonProperty("like")] public int Like { get; set; }
        [JsonProperty("dislike")] public int Dislike { get; set; }
        [JsonProperty("organizer")] public List<ProjectCardDto> EventProjects { get; set; }
        [JsonProperty("projectPlaces")] public List<ProjectMiniDto> ProjectPlaces { get; set; }
        [JsonProperty("ratingFlag")] public int RatingFlag { get; set; }
        [JsonProperty("memberFlag")] public int MemberFlag { get; set; }
    }
}