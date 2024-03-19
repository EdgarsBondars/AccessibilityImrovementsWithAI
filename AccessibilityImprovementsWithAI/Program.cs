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

var toolsLogic = new ToolsLogic();

// toolsLogic.TestOne(client);

Console.WriteLine();