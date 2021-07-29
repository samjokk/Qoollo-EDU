using System;
using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class NewsDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("content")] public string Content { get; set; }
        [JsonProperty("publishDate")] public DateTime PublishDate { get; set; }
    }
}