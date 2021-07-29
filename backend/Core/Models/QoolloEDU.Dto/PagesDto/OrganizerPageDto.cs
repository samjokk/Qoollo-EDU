using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class OrganizerPageDto
    {
        [JsonProperty("organizer")] public OrganizerDto Organizer { get; set; }
        [JsonProperty("previousEvents")] public List<EventCardDto> PreviousEvents { get; set; }
        [JsonProperty("currentEvents")] public List<EventCardDto> CurrentEvents { get; set; }
        [JsonProperty("upcomingEvents")] public List<EventCardDto> UpcomingEvents { get; set; }
        [JsonProperty("contactLinks")] public List<LinkDto> ContactLinks { get; set; }
    }
}