using System.Text.Json;
using Azure.AI.OpenAI;

namespace AccessibilityImprovementsWithAI.ChatGPTInteractions
{
    /// <summary>
    /// Provides a collection of tool definitions to interact with the DOM using AI-driven chat functions.
    /// Each tool is defined with its name, description, and parameters required for execution.
    /// </summary>
    public static class FunctionToolDefinitions
    {
        /// <summary>
        /// Tool definition for clicking an element by its HTTP ID.
        /// </summary>
        public static ChatCompletionsFunctionToolDefinition ClickElementTool = new ChatCompletionsFunctionToolDefinition()
        {
            Name = "click_element",
            Description = "Clicks element by HTTP id",
            Parameters = BinaryData.FromObjectAsJson(
                new
                {
                    Type = "object",
                    Properties = new
                    {
                        ElementId = new
                        {
                            Type = "string",
                            Description = "HTTP element ID property",
                        },
                    },
                    Required = new[] { "elementId" },
                },
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
        };

        /// <summary>
        /// Tool definition for setting focus on an element by its HTTP ID.
        /// </summary>
        public static ChatCompletionsFunctionToolDefinition SetFocusTool = new ChatCompletionsFunctionToolDefinition()
        {
            Name = "set_focus",
            Description = "Sets focus on element by HTTP id",
            Parameters = BinaryData.FromObjectAsJson(
                new
                {
                    Type = "object",
                    Properties = new
                    {
                        ElementId = new
                        {
                            Type = "string",
                            Description = "HTTP element ID property",
                        },
                    },
                    Required = new[] { "elementId" },
                },
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
        };

        /// <summary>
        /// Tool definition for executing JavaScript code in the user's DOM.
        /// The changes are applied instantly and visible to the user.
        /// </summary>
        public static ChatCompletionsFunctionToolDefinition ExecuteJavaScriptTool = new ChatCompletionsFunctionToolDefinition()
        {
            Name = "execute_javascript",
            Description = "Executes provided JavaScript code in users DOM. User will see changes instantly.",
            Parameters = BinaryData.FromObjectAsJson(
                new
                {
                    Type = "object",
                    Properties = new
                    {
                        Script = new
                        {
                            Type = "string",
                            Description = "JavaScript code to execute",
                        },
                    },
                    Required = new[] { "script" },
                },
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
        };

        /// <summary>
        /// Tool definition for retrieving the HTML source of the current page.
        /// </summary>
        public static ChatCompletionsFunctionToolDefinition GetPageHtmlTool = new ChatCompletionsFunctionToolDefinition()
        {
            Name = "get_page_html",
            Description = "Gets the HTML source of the current page",
            Parameters = BinaryData.FromObjectAsJson(
                new
                {
                    Type = "object",
                    Properties = new { },
                    Required = new string[] { },
                },
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
        };
    }
}
