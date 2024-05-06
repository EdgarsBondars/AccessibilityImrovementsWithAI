using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.AI.OpenAI;

namespace AccessibilityImprovementsWithAI.ChatGPTInteractions
{
    public static class FunctionToolDefinitions
    {
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
