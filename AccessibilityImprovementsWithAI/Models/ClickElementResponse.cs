using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccessibilityImprovementsWithAI.Models
{
    public class ClickElementResponse
    {
        [JsonProperty("elementId")]
        public string ElementId { get; set; }
    }
}
