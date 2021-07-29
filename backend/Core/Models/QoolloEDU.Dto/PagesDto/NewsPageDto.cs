using System.Collections.Generic;
using Newtonsoft.Json;

namespace QoolloEDU.Dto.PagesDto
{
    [JsonObject]
    public class NewsPageDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("prevNews")] public NewsDto PrevNews { get; set; }
        [JsonProperty("curNews")] public NewsDto CurNews { get; set; }
        [JsonProperty("nextNews")] public NewsDto NextNews { get; set; }
        [JsonProperty("comments")] public List<CommentDto> Comments { get; set; }

    }
}