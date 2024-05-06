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

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public string GetCurrentFocus()
        {
            // Get the currently focused element using JavaScript
            IWebElement element = (IWebElement)((IJavaScriptExecutor)_driver).ExecuteScript("return document.activeElement;");

            // Return the id of the focused element
            return element.GetAttribute("id");
        }

        public string GetPageHtml()
        {
            // Get the HTML source of the current page
            string pageHtml = _driver.PageSource;

            // Find the start and end index of the <main> tag
            int startIndex = pageHtml.IndexOf("<main>");
            int endIndex = pageHtml.IndexOf("</main>");

            // Extract the content between the <main> tags
            string mainHtml = pageHtml.Substring(startIndex, endIndex - startIndex + 7);

            return mainHtml;
        }

        public string GetCurrentPageHtml()
        {
            // Return the current HTML of the page, including any changes made by JavaScript
            return (string)((IJavaScriptExecutor)_driver).ExecuteScript("return document.documentElement.outerHTML;");
        }

        public void ReplaceHtml(string newHtml)
        {
            // Replace the entire HTML of the page with newHtml
            ((IJavaScriptExecutor)_driver).ExecuteScript($"document.write({newHtml});");
        }

        public object ExecuteJavaScript(string script)
        {
            // Execute the provided JavaScript code
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }
    }
}
