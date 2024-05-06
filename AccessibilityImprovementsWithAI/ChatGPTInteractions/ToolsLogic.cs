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
        private readonly IWebDriver _driver;
        private readonly NavigationLogic _navigationLogic;
        private readonly OpenAIClient _openAiClient;

        public ToolsLogic(IWebDriver driver)
        {
            _driver = driver;
            _navigationLogic = new NavigationLogic(driver);
            _openAiClient = new OpenAIClient(ApiKey, new OpenAIClientOptions());
        }

        public void TestOne(string inputMessage)
        {
            var html = _navigationLogic.GetCurrentPageHtml();
            inputMessage = $"{inputMessage} {Environment.NewLine} {html}";

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-4",
                Messages = 
                {
                    new ChatRequestSystemMessage("You are a accessibility assistant. You have to use given tools to overcome accessibility issues user has encountered."),
                    new ChatRequestSystemMessage("You have to act your primary way of helping is execute_javascript function tool."),
                    new ChatRequestUserMessage(html),
                    new ChatRequestUserMessage(inputMessage)
                },
                Tools = 
                { 
                    FunctionToolDefinitions.ClickElementTool,
                    FunctionToolDefinitions.ExecuteJavaScriptTool,
                    FunctionToolDefinitions.GetPageHtmlTool,
                },
            };

            Response<ChatCompletions> response = _openAiClient.GetChatCompletionsAsync(chatCompletionsOptions).Result;

            ChatChoice responseChoice = response.Value.Choices[0];
            
            while (responseChoice.FinishReason != CompletionsFinishReason.ToolCalls)
            {
                Console.WriteLine(responseChoice.Message.Content);
                var userInput = Console.ReadLine();
                chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(userInput));
                response = _openAiClient.GetChatCompletionsAsync(chatCompletionsOptions).Result;
                responseChoice = response.Value.Choices[0];
            }

            if (responseChoice.FinishReason == CompletionsFinishReason.ToolCalls)
            {
                // Add the assistant message with tool calls to the conversation history
                ChatRequestAssistantMessage toolCallHistoryMessage = new(responseChoice.Message);
                chatCompletionsOptions.Messages.Add(toolCallHistoryMessage);

                // Add a new tool message for each tool call that is resolved
                foreach (ChatCompletionsToolCall toolCall in responseChoice.Message.ToolCalls)
                {
                    chatCompletionsOptions.Messages.Add(GetToolCallResponseMessage(toolCall, FunctionToolDefinitions.ClickElementTool));
                }

                // Now make a new request with all the messages thus far, including the original
                foreach (ChatCompletionsToolCall toolCall in responseChoice.Message.ToolCalls)
                {
                    var functionToolCall = toolCall as ChatCompletionsFunctionToolCall;
                    if (functionToolCall.Name == "click_element" || functionToolCall.Name == "execute_javascript")
                    {
                        return;
                    }
                }
            }
        }

        private ChatRequestToolMessage GetToolCallResponseMessage(ChatCompletionsToolCall toolCall, ChatCompletionsFunctionToolDefinition someTool)
        {
            var functionToolCall = toolCall as ChatCompletionsFunctionToolCall;
            if (functionToolCall?.Name == "click_element")
            {
                // Validate and process the JSON arguments for the function call
                ClickElementResponse clickElementResponse = JsonConvert.DeserializeObject<ClickElementResponse>(functionToolCall.Arguments);
                _navigationLogic.Click(clickElementResponse.ElementId);

                return new ChatRequestToolMessage("Success", toolCall.Id);
            }
            else if (functionToolCall?.Name == "execute_javascript")
            {
                // Validate and process the JSON arguments for the function call
                ExecuteJavaScriptResponse executeJavaScriptResponse = JsonConvert.DeserializeObject<ExecuteJavaScriptResponse>(functionToolCall.Arguments);
                _navigationLogic.ExecuteJavaScript(executeJavaScriptResponse.Script);

                return new ChatRequestToolMessage("Success", toolCall.Id);
            }
            else
            {
                // Handle other or unexpected calls
                throw new NotImplementedException();
            }
        }
    }
}
