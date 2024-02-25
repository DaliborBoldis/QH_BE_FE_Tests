using CoinMarketCapTests.Pages;
using CoinMarketCapTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CoinMarketCapTests.Tests
{
    public class Filtering_Test
    {
        private IWebDriver _driver;
        private FilteringPage _cryptoFilteringPage;

        /// <summary>
        /// Sets up the stage before each test runs.
        /// It initializes the web driver, brings up the FilteringPage, and makes sure CoinMarketCap homepage is loaded.
        /// </summary>

        [SetUp]
        public void Setup()
        {
            _driver = WebDriverSetup.InitializeDriver(true);

            _cryptoFilteringPage = new FilteringPage(_driver);

            _cryptoFilteringPage.LoadWebsite("https://coinmarketcap.com/");
        }

        /// <summary>
        /// This test checks if you can change the number of rows displayed to a specific count.
        /// Precisely, it checks if the row filtering functionality works properly.
        /// The test will run two times - once for 20 rows and once for 50 rows.
        /// </summary>
        /// <param name="rowNumber">The number of rows to select.</param>

        [Test]
        [TestCase("20")]
        [TestCase("50")]
        public void CheckChangeRowsToSpecificCount(string rowNumber)
        {
            // Change rows count and get the number of rows after the change
            int testRowCount = _cryptoFilteringPage.ChangeRowsAndVerify(rowNumber);

            // Confirm that the number of rows after the change is the same as the test case
            Assert.That(testRowCount, Is.EqualTo(int.Parse(rowNumber)), $"The number of rows displayed does not match the expected count of {rowNumber}.");
        }

        /// <summary>
        /// Cleans up after the test is done. It closes the web driver and wraps up the test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Test completed.");

            _driver.Quit();
        }
    }
}
