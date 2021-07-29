using Newtonsoft.Json;

namespace QoolloEDU.Dto
{
    [JsonObject]
    public class MarkdownDto
    {
        [JsonProperty("newMarkdown")] public string NewMarkdown{ get; set; }
    }
}