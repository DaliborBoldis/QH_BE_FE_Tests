using CoinMarketCapTests.Pages;
using CoinMarketCapTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CoinMarketCapTests.Tests
{
    public class Watchlist_Test
    {
        private IWebDriver _driver;
        private WatchlistingPage _coinMarketCapMainPage;

        /// <summary>
        /// Getting the driver ready and loading up the page coinmarketcap.com page.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _driver = WebDriverSetup.InitializeDriver();
            _coinMarketCapMainPage = new WatchlistingPage(_driver);

            _coinMarketCapMainPage.LoadWebsite("https://coinmarketcap.com/");
        }

        /// <summary>
        /// This test randomly picks cryptos to add to watchlist.
        /// It then checks to make sure selected cryptos are actually present in the watchlist page.
        /// </summary>
        [Test]
        public void AddRandomCryptosToWatchlistTest()
        {
            // Add cryptos to the watchlist
            var selectedCryptosData = _coinMarketCapMainPage.AddRandomCryptosToWatchlist();

            // Confirm that between 5 to 10 cryptos are selected
            Assert.That(selectedCryptosData.Count, Is.AtLeast(5).And.AtMost(10), "The number of selected cryptos is not between 5 and 10.");

            // Navigate to the watchlist page and verify the selected cryptos are added to the watchlist
            string verificationResult = _coinMarketCapMainPage.VerifyCryptosOnWatchlist(selectedCryptosData, "https://coinmarketcap.com/watchlist/");
            Assert.That(verificationResult, Is.EqualTo("All selected options added to watchlist"), verificationResult);
        }

        /// <summary>
        /// Cleaning up after the test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Test completed.");

            _driver.Quit();
        }
    }
}
