using OpenQA.Selenium;

namespace AccessibilityImprovementsWithAI.Logic
{
    /// <summary>
    /// Provides navigation and interaction logic for managing and manipulating web pages using Selenium.
    /// </summary>
    public class NavigationLogic
    {
        private readonly IWebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationLogic"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance for browser interactions.</param>
        public NavigationLogic(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Sets focus to the element with the specified ID.
        /// </summary>
        /// <param name="elementId">The ID of the element to focus on.</param>
        public void SetFocus(string elementId)
        {
            IWebElement element = _driver.FindElement(By.Id(elementId));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].focus();", element);
        }

        /// <summary>
        /// Clicks the element with the specified ID.
        /// </summary>
        /// <param name="elementId">The ID of the element to click.</param>
        public void Click(string elementId)
        {
            IWebElement element = _driver.FindElement(By.Id(elementId));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Navigates the browser to the specified URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Retrieves the ID of the currently focused element on the page.
        /// </summary>
        /// <returns>The ID of the currently focused element.</returns>
        public string GetCurrentFocus()
        {
            IWebElement element = (IWebElement)((IJavaScriptExecutor)_driver).ExecuteScript("return document.activeElement;");
            return element.GetAttribute("id");
        }

        /// <summary>
        /// Extracts and returns the HTML content of the <main> tag from the current page.
        /// </summary>
        /// <returns>The HTML content of the <main> tag.</returns>
        public string GetPageHtml()
        {
            string pageHtml = _driver.PageSource;
            int startIndex = pageHtml.IndexOf("<main>");
            int endIndex = pageHtml.IndexOf("</main>");
            string mainHtml = pageHtml.Substring(startIndex, endIndex - startIndex + 7);
            return mainHtml;
        }

        /// <summary>
        /// Retrieves the entire HTML of the current page, including JavaScript modifications.
        /// </summary>
        /// <returns>The HTML content of the current page.</returns>
        public string GetCurrentPageHtml()
        {
            return (string)((IJavaScriptExecutor)_driver).ExecuteScript("return document.documentElement.outerHTML;");
        }

        /// <summary>
        /// Replaces the entire HTML of the current page with the specified HTML.
        /// </summary>
        /// <param name="newHtml">The new HTML content to replace the existing page content.</param>
        public void ReplaceHtml(string newHtml)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript($"document.write({newHtml});");
        }

        /// <summary>
        /// Executes the provided JavaScript code in the context of the current page.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <returns>The result of the executed JavaScript code.</returns>
        public object ExecuteJavaScript(string script)
        {
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }
    }
}
