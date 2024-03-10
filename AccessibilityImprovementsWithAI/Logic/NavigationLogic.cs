using OpenQA.Selenium;

namespace AccessibilityImprovementsWithAI.Logic
{
    public class NavigationLogic
    {
        public void SetFocus(IWebDriver driver, string elementId)
        {
            IWebElement element = driver.FindElement(By.Id(elementId));

            // Set focus on the element using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].focus();", element);
        }

        public void Click(IWebDriver driver, string elementId)
        {
            IWebElement element = driver.FindElement(By.Id(elementId));

            // Set focus on the element using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }
    }
}
