using Newtonsoft.Json;

namespace AccessibilityImprovementsWithAI.Models
{
    /// <summary>
    /// Represents the response containing information about the clicked element.
    /// </summary>
    public class ClickElementResponse
    {
        /// <summary>
        /// ID of the clicked element.
        /// </summary>
        [JsonProperty("elementId")]
        public string ElementId { get; set; }
    }
}
