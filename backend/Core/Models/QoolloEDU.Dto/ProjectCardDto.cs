using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class ProjectCardDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("organizerName")] public string OrganizerName { get; set; }
        [JsonProperty("organizerPhoto")] public string OrganizerPhoto { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("rating")] public int Rating { get; set; }
        [JsonProperty("newsCount")] public int NewsCount { get; set; }
        [JsonProperty("membersCount")] public int MembersCount { get; set; }
        [JsonProperty("creationDate")] public DateTime CreationDate { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
    }
}