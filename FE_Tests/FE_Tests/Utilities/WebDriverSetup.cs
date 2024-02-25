using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CoinMarketCapTests.Utilities
{
    public class WebDriverSetup
    {
        /// <summary>
        /// Initializes up a new web driver, with an option to enable maximized window.
        /// </summary>
        /// <param name="maximized">Flag to indicate whether the browser window should start maximized.</param>
        /// <returns>A new driver instance</returns>
        public static IWebDriver InitializeDriver(bool maximized = false)
        {
            IWebDriver driver = new ChromeDriver();

            if (maximized)
            {
                driver.Manage().Window.Maximize();
            }

            return driver;
        }
    }
}
