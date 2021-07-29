using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class EventDto
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("startDate")] public DateTime StartDate { get; set; }
        [JsonProperty("endDate")] public DateTime EndDate { get; set; }
    }
}