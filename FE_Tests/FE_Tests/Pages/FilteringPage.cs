using OpenQA.Selenium;
using CoinMarketCapTests.Utilities;

namespace CoinMarketCapTests.Pages
{
    /// <summary>
    /// FilteringPage provides functionality specific to the cryptocurrency filtering interface of the application.
    /// It extends BasePage, utilizing its methods for web interactions, and adds methods specific to filtering operations,
    /// such as changing the number of rows displayed and verifying the changes.
    /// </summary>
    public class FilteringPage : BasePage
    {
        public FilteringPage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Loads the website with the given URL, utilizing the Navigate method from BasePage.
        /// </summary>
        /// <param name="url">The URL of the website to load.</param>
        public void LoadWebsite(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Changes the number of rows displayed in the filtering interface and verifies the change.
        /// </summary>
        /// <param name="rowNumber">The desired number of rows to display.</param>
        /// <returns>The actual number of rows displayed after the change.</returns>
        public int ChangeRowsAndVerify(string rowNumber)
        {
            var changeRowsCountElement = SelectRowCountOption();

            if (changeRowsCountElement == null)
            {
                return 0;
            }

            ClickElement(changeRowsCountElement);

            int rowCount = VerifyRowCount(rowNumber);

            return rowCount;
        }

        /// <summary>
        /// Selects a dropdown option for changing the number of rows displayed in the table.
        /// This method looks for elements that match the criteria for row count options (e.g., "20", "50", "100") and selects one at random.
        /// </summary>
        /// <returns>A web element representing the randomly selected row count option, or null if no suitable options are found.</returns>
        private IWebElement? SelectRowCountOption()
        {
            {
                var elements = FindElements(By.XPath("//*[@data-sensors-click='true']"))
    .Where(el => new[] { "20", "50", "100" }.Contains(el.Text.Trim()))
    .ToList();

                if (!elements.Any())
                {
                    return null; // Return null if no element found
                }

                Random? random = new();

                return elements[random.Next(elements.Count)];
            }
        }

        /// <summary>
        /// Clicks on the specified web element, simulating a user interaction.
        /// This method is used after selecting a row count option to apply the change.
        /// </summary>
        /// <param name="element">The web element to be clicked.</param>
        private void ClickElement(IWebElement element)
        {
            element.Click();

            WaitForElementDisplayed(By.ClassName("tippy-content"), 10);
        }

        /// <summary>
        /// Verifies that the number of rows currently displayed in the table matches the expected row count.
        /// If the displayed row count doesn't match the expected count, this method's going to let you know something's off.
        /// </summary>
        /// <param name="expectedRowCount">The expected number of rows to be displayed in the table.</param>
        /// <returns>The actual number of rows displayed if it matches the expected count, throwing an exception otherwise.</returns>
        private int VerifyRowCount(string expectedRowCount)
        {
            var button = FindElements(By.XPath("//div[contains(@class, 'tippy-content')]//button[@data-sensors-click='true']"))
                .FirstOrDefault(button => button.Text.Trim() == expectedRowCount);

            if (button == null)
            {
                return 0; // Return 0 if no button found
            }

            button.Click();

            return WaitForRowCountToMatch(expectedRowCount);
        }

        // <summary>
        /// Waits for the table's row count to match the expected value, checking repeatedly up to a maximum number of retries.
        /// This method executes a JavaScript script to get the current row count and compares it to the expected count.
        /// If they don't match up right away, it sleeps for a bit and tries again, up to the max retries of 3.
        /// If the count still doesn't match after all attempts, it throws an exception.
        /// </summary>
        /// <param name="expectedRowCount">The expected number of rows to be displayed.</param>
        /// <param name="maxRetries">The maximum number of times to check the row count, because it seems the first look isn't enough.</param>
        /// <param name="retryInterval">The waiting time between each check, giving the page some breathing room to get its act together.</param>
        /// <returns>The actual number of rows if it matches the expected count.</returns>
        private int WaitForRowCountToMatch(string expectedRowCount, int maxRetries = 3, int retryInterval = 1000)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                string jsScript = JavaScriptLoader.Load(@"getCryptoRowsFromTable");
                var rowCountResult = ExecuteJavaScript(jsScript);

                if (rowCountResult is IReadOnlyCollection<object> rowsData && rowsData.Count.ToString() == expectedRowCount)
                {
                    return rowsData.Count; // Return row count if it matches the expected count
                }

                Thread.Sleep(retryInterval);
            }

            throw new InvalidOperationException("Failed to verify row count within the allowed attempts.");
        }
    }
}
