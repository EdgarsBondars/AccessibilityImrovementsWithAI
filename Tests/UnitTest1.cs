using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestName()
        {
            IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("http://yourwebsite.com");
            // Add your test code here
            driver.Quit();
        }
    }
}