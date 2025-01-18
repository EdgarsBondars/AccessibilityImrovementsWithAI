using AccessibilityImprovementsWithAI.Logic;
using AccessibilityImprovementsWithAI.Models;
using Azure;
using Azure.AI.OpenAI;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace AccessibilityImprovementsWithAI.ChatGPTInteractions
{
    /// <summary>
    /// Contains logic for integrating ChatGPT interactions with AI tools and navigation logic to address accessibility issues.
    /// </summary>
    public class ToolsLogic
    {
        private readonly IWebDriver _driver;
        private readonly NavigationLogic _navigationLogic;
        private readonly OpenAIClient _openAiClient;

        /// <summary>
        /// Contains logic for integrating ChatGPT interactions with AI tools and navigation logic to address accessibility issues.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance for interacting with the browser.</param>
        public ToolsLogic(IWebDriver driver)
        {
            _driver = driver;
            _navigationLogic = new NavigationLogic(driver);
            _openAiClient = new OpenAIClient("ApiKey", new OpenAIClientOptions());
        }

        /// <summary>
        /// Attempts to fix an accessibility issue described by the user by using AI tools.
        /// Combines the user's input message with the current page's HTML, sends the information to the OpenAI service,
        /// and processes responses to resolve the described accessibility issue.
        /// </summary>
        /// <param name="inputMessage">The user-provided description of the accessibility issue.</param>
        public void FixDescribedAccessibilityIssue(string inputMessage)
        {
            // Retrieve the HTML of the current page and append it to the input message.
            var html = _navigationLogic.GetCurrentPageHtml();
            inputMessage = $"{inputMessage} {Environment.NewLine} {html}";

            // Set up ChatGPT request options including the accessibility tools.
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-4",
                Messages =
                {
                    new ChatRequestSystemMessage("You are an accessibility assistant. Use the given tools to address accessibility issues."),
                    new ChatRequestSystemMessage("Your primary method for resolving issues is the execute_javascript function tool."),
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

            // Send the request to OpenAI and process the response.
            Response<ChatCompletions> response = _openAiClient.GetChatCompletionsAsync(chatCompletionsOptions).Result;
            ChatChoice responseChoice = response.Value.Choices[0];

            // Handle responses until tool calls are made.
            while (responseChoice.FinishReason != CompletionsFinishReason.ToolCalls)
            {
                Console.WriteLine(responseChoice.Message.Content);
                var userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(userInput));
                    response = _openAiClient.GetChatCompletionsAsync(chatCompletionsOptions).Result;
                    responseChoice = response.Value.Choices[0];
                }
            }

            // Process tool calls made by ChatGPT.
            if (responseChoice.FinishReason == CompletionsFinishReason.ToolCalls)
            {
                // Add the assistant's tool call response to the conversation history.
                ChatRequestAssistantMessage toolCallHistoryMessage = new(responseChoice.Message);
                chatCompletionsOptions.Messages.Add(toolCallHistoryMessage);

                // Process each tool call and handle their responses.
                foreach (ChatCompletionsToolCall toolCall in responseChoice.Message.ToolCalls)
                {
                    chatCompletionsOptions.Messages.Add(GetToolCallResponseMessage(toolCall, FunctionToolDefinitions.ClickElementTool));
                }

                // Check for specific tool calls (e.g., click_element, execute_javascript) and return if processed.
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

        /// <summary>
        /// Processes a tool call and returns the corresponding response message.
        /// </summary>
        /// <param name="toolCall">The tool call to process.</param>
        /// <param name="someTool">The tool definition being used.</param>
        /// <returns>A <see cref="ChatRequestToolMessage"/> indicating the result of the tool call.</returns>
        private ChatRequestToolMessage GetToolCallResponseMessage(ChatCompletionsToolCall toolCall, ChatCompletionsFunctionToolDefinition someTool)
        {
            var functionToolCall = toolCall as ChatCompletionsFunctionToolCall;
            if (functionToolCall?.Name == "click_element")
            {
                // Handle click_element tool call.
                ClickElementResponse clickElementResponse = JsonConvert.DeserializeObject<ClickElementResponse>(functionToolCall.Arguments);
                _navigationLogic.Click(clickElementResponse.ElementId);

                return new ChatRequestToolMessage("Success", toolCall.Id);
            }
            else if (functionToolCall?.Name == "execute_javascript")
            {
                // Handle execute_javascript tool call.
                ExecuteJavaScriptResponse executeJavaScriptResponse = JsonConvert.DeserializeObject<ExecuteJavaScriptResponse>(functionToolCall.Arguments);
                _navigationLogic.ExecuteJavaScript(executeJavaScriptResponse.Script);

                return new ChatRequestToolMessage("Success", toolCall.Id);
            }
            else
            {
                // Throw exception for unsupported or unimplemented tool calls.
                throw new NotImplementedException();
            }
        }
    }
}