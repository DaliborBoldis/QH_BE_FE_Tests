using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CoinMarketCapTests.Exceptions;
using System.Diagnostics;

namespace CoinMarketCapTests.Pages
{
    /// <summary>
    /// BasePage serves as the foundational class for all page-specific classes within the test framework.
    /// It provides common web driver interactions such as navigation, element finding, and executing JavaScript,
    /// ensuring a consistent approach across different pages.
    /// </summary>
    public class BasePage
    {
        // Constructor to initialize the web driver
        protected readonly IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        /// <summary>
        /// Navigates to the specified URL. Throws a NavigationException if navigation fails.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        protected void Navigate(string url)
        {
            try
            {
                Driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException ex)
            {
                LogError($"Failed to navigate to {url}", ex);
                throw new NavigationException($"Failed to navigate to the following URL: {url}", ex);
            }
        }

        /// <summary>
        /// Opens a new tab in the browser. Throws a NewTabException if unable to open a new tab.
        /// </summary>
        protected void OpenNewTab()
        {
            try
            {
                Driver.SwitchTo().NewWindow(WindowType.Tab);
            }
            catch (Exception ex)
            {
                LogError("Failed to open a new tab", ex);
                throw new NewTabException("WebDriver failed to open a new tab.", ex);
            }
        }

        /// <summary>
        /// Finds a web element using the provided locator. Throws an ElementNotFoundException if the element is not found.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <returns>The found web element.</returns>
        protected IWebElement FindElement(By by)
        {
            try
            {
                return Driver.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                LogError($"Element by {by} not found on the page", ex);
                throw new ElementNotFoundException($"Element by {by} not found on the page", ex);
            }
        }

        /// <summary>
        /// Finds all web elements matching the provided locator. Throws an ElementsNotFoundException if no elements are found.
        /// </summary>
        /// <param name="by">The locator used to find the elements.</param>
        /// <returns>A read-only collection of found web elements.</returns>
        protected IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return Driver.FindElements(by);
            }
            catch (NoSuchElementException ex)
            {
                LogError($"Elements by {by} not found on the page", ex);
                throw new ElementsNotFoundException($"Elements by {by} not found on the page", ex);
            }
        }

        /// <summary>
        /// Executes JavaScript on the current page. Throws a JavaScriptExecutionException if execution fails.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments to pass to the JavaScript code.</param>
        /// <returns>The result of the JavaScript execution.</returns>
        protected object ExecuteJavaScript(string script, params object[] args)
        {
            try
            {
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
                return jsExecutor.ExecuteScript(script, args);
            }
            catch (Exception ex)
            {
                LogError($"JavaScript execution failed for script: {script}", ex);
                throw new JavaScriptExecutionException("Failed to execute JavaScript.", ex);
            }
        }

        /// <summary>
        /// Waits for an element to be displayed within a specified timeout. Throws ElementNotDisplayedException, ElementNotFoundException, or UnexpectedElementException based on the encountered scenario.
        /// </summary>
        /// <param name="by">The locator of the element to wait for.</param>
        /// <param name="timeoutInSeconds">The maximum time to wait for the element to be displayed.</param>
        /// <returns>True if the element is displayed within the timeout, otherwise false.</returns>
        public bool WaitForElementDisplayed(By by, int timeoutInSeconds)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by).Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                LogError($"Element by {by} not displayed within {timeoutInSeconds} seconds");
                throw new ElementNotDisplayedException($"Element by {by} not displayed within {timeoutInSeconds} seconds");
            }
            catch (NoSuchElementException)
            {
                LogError($"Element by {by} not found on the page");
                throw new ElementNotFoundException($"Element by {by} not found on the page");
            }
            catch (Exception ex)
            {
                LogError($"An unexpected error occurred while waiting for element by {by}", ex);
                throw new UnexpectedElementException($"An unexpected error occurred while waiting for element by {by}", ex);
            }
        }

        // Basic logging method for demonstration purposes
        public static void LogError(string message, Exception? ex = null)
        {
            // In a real application, we might consider using a more robust logging framework
            Debug.WriteLine($"Error: {message}");

            if (ex != null) Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
