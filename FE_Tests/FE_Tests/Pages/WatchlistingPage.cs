using OpenQA.Selenium;
using System;
using CoinMarketCapTests.Utilities;
using CoinMarketCapTests.Exceptions;

namespace CoinMarketCapTests.Pages
{
    /// <summary>
    /// WatchlistingPage handles functionalities related to the cryptocurrency watchlist feature.
    /// It allows for adding cryptos to the watchlist, verifying their presence, and managing watchlist entries.
    /// This class encapsulates the steps needed to simulate user interactions for watchlisting cryptocurrencies,
    /// including selecting random cryptos, adding them to the watchlist, and ensuring they are correctly saved.
    /// </summary>
    public class WatchlistingPage : BasePage
    {
        public WatchlistingPage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Navigates to the specified URL to load the webpage. This is the initial step to set the stage for further actions on the watchlist page.
        /// </summary>
        /// <param name="url">The URL of the webpage to load, where the watchlisting action happens.</param>
        public void LoadWebsite(string url)
        {
            Navigate(url);

            ScrollToLoadAllRows();
        }

        /// <summary>
        /// Randomly selects random cryptocurrencies and adds them to the watchlist by interacting with the 'star' icons.
        /// The method returns the data of the selected cryptos for further verification.
        /// </summary>
        /// <returns>A list of lists, where each inner list contains details of a cryptocurrency added to the watchlist.</returns>
        public List<List<string>> AddRandomCryptosToWatchlist()
        {
            try
            {
                // Pick and star random cryptos on the list
                string jsScript = JavaScriptLoader.Load(@"PickRandomRowsAndClickStars");

                if (ExecuteJavaScript(jsScript) is not IReadOnlyCollection<object> result) throw new Exception("JavaScript returned an unexpected result.");

                // Return a list of cryptocurrencies randomly selected by the Javascript script
                return result.Select(row => ((IReadOnlyCollection<object>)row)
                              .Select(cellData => cellData?.ToString() ?? string.Empty)
                              .ToList())
                             .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while adding cryptos to watchlist: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// After the cryptos have been added to the watchlist, this method checks the watchlist to make sure they're all there.
        /// If something's wrong, it throws an error, letting us know that one of our cryptos didn't make it to the list.
        /// </summary>
        /// <param name="watchlistedCryptos">The data of the cryptocurrencies expected to be found in the watchlist.</param>
        /// <param name="url">The URL of the watchlist page to verify the selected cryptos against.</param>
        /// <returns>A success message if all selected cryptos are found in the watchlist.</returns>
        public string VerifyCryptosOnWatchlist(List<List<string?>> watchlistedCryptos, string url)
        {
            OpenNewTab();

            Navigate(url);

            string jsScript = JavaScriptLoader.Load(@"getCryptoRowsFromTable");

            if (ExecuteJavaScript(jsScript) is not IReadOnlyCollection<object> watchlistData)
            {
                throw new JavaScriptExecutionException("Failed to retrieve cryptos from watchlist.");
            }

            // Cryptos that are found in the watchlist table
            var cryptosInWatchlist = watchlistData.Select(row => ((IReadOnlyCollection<object>)row)
                                           .Select(cell => cell.ToString()).ToList()).ToList();

            // Print both lists to Console for comparison
            PrintListContents(watchlistedCryptos, "Previously Watchlisted Cryptos");
            PrintListContents(cryptosInWatchlist, "Cryptos found in Watchlist");

            // Compare expected and watchlisted cryptos to determine whether something's missing
            foreach (var expectedCrypto in watchlistedCryptos)
            {
                if (!cryptosInWatchlist.Any(crypto => crypto[0] == expectedCrypto[0] && crypto[1] == expectedCrypto[1]))
                {
                    throw new CryptoNotFoundException($"Missing crypto on Watchlist with ID: {expectedCrypto[0]} and Name: {expectedCrypto[1]}");
                }
            }

            // Return the success message if all previously selected (starred) cryptos are found in the watchlist page
            return "All selected options added to watchlist";
        }

        /// <summary>
        /// Scrolls through the page, making sure every last row of cryptos is loaded and visible.
        /// </summary>
        private void ScrollToLoadAllRows()
        {
            string js_ScrollScript = JavaScriptLoader.Load(@"scrollToBottom");
            ExecuteJavaScript(js_ScrollScript);

            // Wait until scrolling is complete
            bool scrollComplete = false;
            while (!scrollComplete)
            {
                Thread.Sleep(1000); // Instead of using Sleep, there might be better approaches to this problem. But hey... It works.
                scrollComplete = (bool)ExecuteJavaScript("return window.scrollComplete === true;");
            }
        }

        // Print content of lists to Console
        private static void PrintListContents(List<List<string?>> list, string header)
        {
            Console.WriteLine($"-----{header}-----");
            foreach (var row in list)
            {
                string rowInfo = string.Join(", ", row);
                Console.WriteLine(rowInfo);
            }
        }
    }
}
