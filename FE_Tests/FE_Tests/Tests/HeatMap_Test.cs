using CoinMarketCapTests.Pages;
using CoinMarketCapTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CoinMarketCapTests.Tests
{
    public class HeatMap_Test
    {
        private IWebDriver _driver;
        private HeatMapPage _cryptoHeatMapPage;

        /// <summary>
        /// Sets up the web driver and navigates to the heat map page.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _driver = WebDriverSetup.InitializeDriver();

            _cryptoHeatMapPage = new HeatMapPage(_driver);

            _cryptoHeatMapPage.OpenHeatMap("https://coinmarketcap.com/");
        }

        /// <summary>
        /// This test makes sure the 'heatmapchart' button is clicked and the heat map loaded,
        /// then it checks if the crypto heatmap contains elements (cryptos) in form of 'Gwrap' classes.
        /// </summary>
        [Test]
        public void VerifyHeatMapChartLoaded()
        {
            // Asserts that the Heat Map Chart is loaded.
            Assert.That(_cryptoHeatMapPage.IsHeatMapChartElementLoaded(), "The 'HeatMapChart' element didn't load or does not exist within the page.");

            // Counts the 'gwrap' classes and asserts the count is greater than zero.
            int count = _cryptoHeatMapPage.CountGwrapClasses();
            Assert.That(count, Is.GreaterThan(0), "Gwrap classes within HeatMapChart element are not found.");
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
