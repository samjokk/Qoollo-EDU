using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class SearchEventPageDto
    {
        [JsonProperty("topEvents")] public List<EventCardMiniDto> TopEvents { get; set; }
        [JsonProperty("events")] public List<EventCardDto> Events { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
    }
}