using OpenQA.Selenium;

namespace CoinMarketCapTests.Pages
{
    /// <summary>
    /// HeatMapPage is tasked with managing interactions on the cryptocurrency heat map page.
    /// It extends BasePage and provides methods to navigate to the heat map, verify elements, and interact with the map's features.
    /// </summary>
    public class HeatMapPage : BasePage
    {
        public HeatMapPage(IWebDriver driver) : base(driver) { }

        private IWebElement TreeMapTab => Driver.FindElement(By.ClassName("treeMapTab"));
        private IWebElement HeatMapChart => Driver.FindElement(By.ClassName("heatMapChart"));

        /// <summary>
        /// Navigates to the specified heat map page URL and ensures the TreeMap visualization tab is visible.
        /// It encapsulates the navigation logic and error handling to provide a seamless transition to the heat map page.
        /// In case of navigation or loading issues, appropriate exceptions are thrown to signal potential problems with the page or the connection.
        /// </summary>
        /// <param name="url">The URL of the heat map page to navigate to.</param>
        public void OpenHeatMap(string url)
        {
            try
            {
                Navigate(url);

                WaitForElementDisplayed(By.ClassName("treeMapTab"), 10);

                TreeMapTab.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to open the heatmap page.", ex);
            }
        }

        // Checks if the TreeMap element is loaded and visible on the page.
        public bool IsTreeMapElementLoaded() => TreeMapTab != null;

        // Verifies if the HeatMap chart element is successfully loaded and displayed.
        public bool IsHeatMapChartElementLoaded() => HeatMapChart.Displayed;

        // Counts the number of 'gwrap' classes, which are part of the heat map visualization structure.
        public int CountGwrapClasses()
        {
            return HeatMapChart.FindElements(By.ClassName("gwrap")).Count;
        }
    }
}
