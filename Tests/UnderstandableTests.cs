using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using AccessibilityImprovementsWithAI.ChatGPTInteractions;

namespace Tests
{
    public class UnderstandableTests
    {
        /// <summary>
        /// Guidelines tested: 
        /// 3.1.1 - Language of a Page,
        /// 4.1.2 - Name, Role, Value,
        /// 4.1.3 - Status Messages
        /// Not passed:
        /// 3.3.5 Help
        /// 3.3.6 Error Prevention (All)
        /// </summary>
        /// Passed successfully 
        /// Generated response: {
        /// document.querySelector('html').setAttribute('lang', 'en');
        /// document.querySelector('#username').setAttribute('aria-label', 'Username');
        /// document.querySelector('button').setAttribute('aria-label', 'Submit button');
        /// document.querySelector('button').setAttribute('role', 'button');
        /// document.querySelector('button').addEventListener('click', function(event) {
        ///    alert('Form submitted successfully!');
        /// });

        [Test]
        public void Understandable()
        {
            IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://localhost:7299/Understandable");
            var toolsLogic = new ToolsLogic(driver);

            toolsLogic.FixDescribedAccessibilityIssue("If possible, using javascript fix accessibility issues.");

            driver.Quit();
        }
    }
}