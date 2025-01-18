using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using AccessibilityImprovementsWithAI.ChatGPTInteractions;

namespace Tests
{
    public class RobustTests
    {
        /// <summary>
        /// Guidelines tested: 
        /// 4.1.1 - Parsing 
        /// 4.1.2 - Name, Role, Value,
        /// 4.1.3 - Status Messages
        /// </summary>
        /// Passed successfully 
        /// Generated response: {
        /// document.querySelector('input#username').outerHTML = '<input type="text" id="username"/>';
        /// document.querySelector('#username').setAttribute('aria-label', 'Username');
        /// document.querySelector('button').setAttribute('aria-label', 'Submit button');
        /// document.querySelector('button').setAttribute('role', 'button');
        /// document.querySelector('button').addEventListener('click', function(event) {
        ///    alert('Form submitted successfully!');
        /// });

        [Test]
        public void Robust()
        {
            IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://localhost:7299/Robust");
            var toolsLogic = new ToolsLogic(driver);

            toolsLogic.FixDescribedAccessibilityIssue("If possible, using javascript fix accessibility issues.");

            driver.Quit();
        }
    }
}