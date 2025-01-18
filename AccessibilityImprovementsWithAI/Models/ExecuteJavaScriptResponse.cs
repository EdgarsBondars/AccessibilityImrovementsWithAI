using Newtonsoft.Json;

namespace AccessibilityImprovementsWithAI.Models
{
    /// <summary>
    /// Represents the response containing JavaScript code to be executed.
    /// </summary>
    public class ExecuteJavaScriptResponse
    {
        /// <summary>
        /// Gets or sets the JavaScript code to execute.
        /// </summary>
        [JsonProperty("script")]
        public string Script { get; set; }
    }
}
