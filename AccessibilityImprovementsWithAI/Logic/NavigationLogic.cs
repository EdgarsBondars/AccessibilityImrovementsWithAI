using OpenQA.Selenium;

namespace AccessibilityImprovementsWithAI.Logic
{
    public class NavigationLogic
    {
        private readonly IWebDriver _driver;

        public NavigationLogic(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SetFocus(string elementId)
        {
            IWebElement element = _driver.FindElement(By.Id(elementId));

            // Set focus on the element using JavaScript
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].focus();", element);
        }

        public void Click(string elementId)
        {
            IWebElement element = _driver.FindElement(By.Id(elementId));

            // Set focus on the element using JavaScript
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
        }
    }
}
