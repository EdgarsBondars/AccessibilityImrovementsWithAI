using AccessibilityImprovementsWithAI.ChatGPTInteractions;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

IConfiguration config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var ApiKey = config["ApiKey"];
OpenAIClient client = new OpenAIClient(ApiKey, new OpenAIClientOptions());
IWebDriver driver = new EdgeDriver();

var toolsLogic = new ToolsLogic(driver);
// Navigate to a website
driver.Navigate().GoToUrl("https://localhost:7299/");

string htmlContent = driver.PageSource;

Console.WriteLine(htmlContent);

// Close the WebDriver session
//toolsLogic.TestOne(client);

Console.ReadLine();
string input = string.Empty;
while (input != "exit")
{
    /*IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
    IWebElement element = (IWebElement)js.ExecuteScript("return document.activeElement");
    string currentElement = driver.SwitchTo().ActiveElement().Text;
*/
    toolsLogic.TestOne(input);
    Console.WriteLine("Problem solved? New chat is initiated.");
    input = Console.ReadLine();
}

driver.Quit();
