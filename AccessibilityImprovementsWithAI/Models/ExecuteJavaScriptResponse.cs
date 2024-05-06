using Newtonsoft.Json;

namespace AccessibilityImprovementsWithAI.Models
{
    public class ExecuteJavaScriptResponse
    {
        [JsonProperty("script")]
        public string Script { get; set; }
    }
}
