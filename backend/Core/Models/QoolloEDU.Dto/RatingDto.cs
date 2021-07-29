using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class RatingDto
    {
        [JsonProperty("rating")] public int Rating { get; set; }
    }
}