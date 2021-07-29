using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class CommentDto
    {
        [JsonProperty("user")] public UserMiniDto User { get; set; }
        [JsonProperty("comment")] public string Comment { get; set; }
    }
}