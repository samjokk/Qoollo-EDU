using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class SearchDeveloperPageDto
    {
        [JsonProperty("developers")] public List<UserCardDto> Developers { get; set; }
        [JsonProperty("tags")] public List<TagDto> Tags { get; set; }
    }
}