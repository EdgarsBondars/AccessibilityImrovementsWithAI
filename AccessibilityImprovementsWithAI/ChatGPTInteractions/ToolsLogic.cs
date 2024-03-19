using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AccessibilityImprovementsWithAI.Logic;
using AccessibilityImprovementsWithAI.Models;
using Azure;
using Azure.AI.OpenAI;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace AccessibilityImprovementsWithAI.ChatGPTInteractions
{
    public class ToolsLogic
    {
        public void TestOne(OpenAIClient client)
        {
            var clickElementTool = new ChatCompletionsFunctionToolDefinition()
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

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-4",
                Messages = { new ChatRequestUserMessage("Click date time field <input id=\"exampleDate\" class=\"datepicker picker__input\" name=\"date\" type=\"text\" value=\"14 August, 2023\" data-value=\"2023-08-08\" readonly=\"\" aria-haspopup=\"true\" aria-readonly=\"false\" aria-owns=\"exampleDate_root\">") },
                Tools = { clickElementTool },
            };

            Response<ChatCompletions> response = client.GetChatCompletionsAsync(chatCompletionsOptions).Result;

            ChatChoice responseChoice = response.Value.Choices[0];
            if (responseChoice.FinishReason == CompletionsFinishReason.ToolCalls)
            {
                // Add the assistant message with tool calls to the conversation history
                ChatRequestAssistantMessage toolCallHistoryMessage = new(responseChoice.Message);
                chatCompletionsOptions.Messages.Add(toolCallHistoryMessage);

                // Add a new tool message for each tool call that is resolved
                foreach (ChatCompletionsToolCall toolCall in responseChoice.Message.ToolCalls)
                {
                    //chatCompletionsOptions.Messages.Add(GetToolCallResponseMessage(toolCall, clickElementTool));
                }

                // Now make a new request with all the messages thus far, including the original
            }
        }

        /*private ChatRequestToolMessage GetToolCallResponseMessage(ChatCompletionsToolCall toolCall, ChatCompletionsFunctionToolDefinition someTool)
        {
            var navigationLogic = new NavigationLogic();

            var functionToolCall = toolCall as ChatCompletionsFunctionToolCall;
            if (functionToolCall?.Name == "click_element")
            {
                // Validate and process the JSON arguments for the function call
                ClickElementResponse clickElementResponse = JsonConvert.DeserializeObject<ClickElementResponse>(functionToolCall.Arguments);
                var functionResultData = navigationLogic.Click(new WebDriver(), clickElementResponse.ElementId);
                return new ChatRequestToolMessage(functionResultData.ToString(), toolCall.Id);
            }
            else
            {
                // Handle other or unexpected calls
                throw new NotImplementedException();
            }
        }*/
    }
}
