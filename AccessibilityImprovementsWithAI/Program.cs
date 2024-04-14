using AccessibilityImprovementsWithAI.ChatGPTInteractions;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using static System.Environment;

IConfiguration config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var ApiKey = config["ApiKey"];
OpenAIClient client = new OpenAIClient(ApiKey, new OpenAIClientOptions());
IWebDriver driver = new ChromeDriver();

var toolsLogic = new ToolsLogic(driver);
// Navigate to a website
driver.Navigate().GoToUrl("https://localhost:7299/");

string htmlContent = driver.PageSource;

Console.WriteLine(htmlContent);

// Close the WebDriver session
toolsLogic.TestOne(client);

Console.ReadLine();
driver.Quit();
