using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class AchievementDto
    {
        [JsonProperty("projectId")] public int ProjectId { get; set; }
        [JsonProperty("projectName")] public string ProjectName { get; set; }
        [JsonProperty("place")] public int Place { get; set; }
    }
}