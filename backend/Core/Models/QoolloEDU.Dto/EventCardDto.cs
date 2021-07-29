using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class EventCardDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("organizerName")] public string OrganizerName { get; set; }
        [JsonProperty("organizerPhoto")] public string OrganizerPhoto { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("rating")] public int Rating { get; set; }
        [JsonProperty("projectCount")] public int ProjectCount { get; set; }
        [JsonProperty("membersCount")] public int MembersCount { get; set; }
        [JsonProperty("startDate")] public DateTime StartDate { get; set; }
        [JsonProperty("endDate")] public DateTime EndDate { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
    }
}