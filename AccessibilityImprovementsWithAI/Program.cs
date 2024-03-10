using AccessibilityImprovementsWithAI.ChatGPTInteractions;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using static System.Environment;

IConfiguration config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var ApiKey = config["ApiKey"];

OpenAIClient client = new OpenAIClient(ApiKey, new OpenAIClientOptions());

/*var chatCompletionsOptions = new ChatCompletionsOptions()
{
    DeploymentName = "gpt-4", //This must match the custom deployment name you chose for your model
    Messages =
    {
        new ChatRequestSystemMessage("You are a helpful assistant."),
        new ChatRequestUserMessage("Does Azure OpenAI support customer managed keys?"),
        new ChatRequestAssistantMessage("Yes, customer managed keys are supported by Azure OpenAI."),
        new ChatRequestUserMessage("Do other Azure AI services support this too?"),
    },
    MaxTokens = 100
};

Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);

Console.WriteLine(response.Value.Choices[0].Message.Content);*/

var toolsLogic = new ToolsLogic();

toolsLogic.TestOne(client);

Console.WriteLine();