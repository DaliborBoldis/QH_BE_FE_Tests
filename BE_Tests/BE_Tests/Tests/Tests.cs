using Newtonsoft.Json;
using NUnit.Framework;
using static RestAssured.Dsl;
using BE_Tests.Models;
using NUnit.Framework.Legacy;

namespace BE_Tests.Tests
{
    /// <summary>
    /// Tests for cryptocurrency data retrieval and conversion using CoinMarketCap API.
    /// </summary>
    [TestFixture]
    public class CryptoCurrencyTests
    {
        private const string ApiKey = "__API_KEY_HERE__";
        private const string BaseUrl = "https://pro-api.coinmarketcap.com";

        /// <summary>
        /// Generic method to call the CoinMarketCap API and handle the response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response into.</typeparam>
        /// <param name="endpoint">The API endpoint to call.</param>
        /// <param name="setupAction">Additional setup actions for the request, if any.</param>
        /// <returns>The deserialized response.</returns>
        /// <exception cref="ApiResponseException">Thrown if the API call fails.</exception>
        /// <exception cref="DeserializationException">Thrown if deserialization fails.</exception>
        private static async Task<T> GetApiResponse<T>(string endpoint, Action<dynamic>? setupAction)
        {
            var request = Given().Header("X-CMC_PRO_API_KEY", ApiKey);
            setupAction?.Invoke(request);

            // Get response from API
            var response = request.When().Get(BaseUrl + endpoint).Then().Extract().Response();

            // Check the response and if NOT OK, throw an exception to a caller method
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApiResponseException($"API call failed with status code {response.StatusCode} and content {await response.Content.ReadAsStringAsync()}", response.StatusCode);
            }

            // Read and deserialize the response content
            try
            {
                string content = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(content) ?? throw new InvalidOperationException("Deserialization resulted in a null object.");
                return result;
            }
            catch (JsonSerializationException ex)
            {
                throw new DeserializationException("Error deserializing the response content.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred during the API response handling.", ex);
            }
        }

        /// <summary>
        /// Test to convert cryptocurrencies to Boliviano (BOB) and print their prices.
        /// </summary>
        [Test]
        public async Task ConvertToBOB()
        {
            const string cryptos = "BTC,ETH,USDT";

            // Retrieve crypto IDs from the API - https://coinmarketcap.com/api/documentation/v1/#operation/getV1CryptocurrencyMap
            CryptoResponse cryptoResponse = await GetApiResponse<CryptoResponse>($"/v1/cryptocurrency/map?symbol={cryptos}", null);

            // Loop through cryptos and convert them to BOB - https://coinmarketcap.com/api/documentation/v1/#operation/getV1ToolsPriceconversion
            foreach (var crypto in cryptoResponse.Data)
            {
                // Call the API to convert specified ID to BOB
                ConversionResponse conversion = await GetApiResponse<ConversionResponse>("/v1/tools/price-conversion", request =>
                {
                    request.QueryParam("id", crypto.Id)
                           .QueryParam("amount", 1)
                           .QueryParam("convert", "BOB");
                });

                // Output the conversion result to console
                Console.WriteLine($"{crypto.Name} with ID {crypto.Id} and ({crypto.Symbol}): {conversion.Data.Quote.BOB.Price} BOB");
            }
        }

        /// <summary>
        /// Test to validate specific details about Ethereum from the CoinMarketCap API.
        /// </summary>
        [Test]
        public async Task EthereumInfoValidation()
        {
            const int ethereumId = 1027;

            // Retrieve the cryptocurrency info from the API - https://coinmarketcap.com/api/documentation/v1/#operation/getV1CryptocurrencyInfo
            var cryptoInfoResponse = await GetApiResponse<CryptoInfoResponse>($"/v1/cryptocurrency/info?id={ethereumId}", null);

            var ethereumInfo = cryptoInfoResponse.Data[ethereumId.ToString()];

            // Confirm that the specified logo URL is present
            Assert.That(ethereumInfo.Logo, Is.EqualTo("https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png"), $"Ethereum logo mismatch.");

            // Confirm the technical doc is present
            Assert.That(ethereumInfo.Urls.Technical_doc.FirstOrDefault(), Is.EqualTo("https://github.com/ethereum/wiki/wiki/White-Paper"), $"Ethereum 'Technical doc' is not present.");

            // Confirm the symbol of the currency
            Assert.That(ethereumInfo.Symbol, Is.EqualTo("ETH"), $"Cryptocurrency symbol mismatch.");

            // Confirm the date added
            Assert.That(ethereumInfo.Date_added, Is.EqualTo("2015-08-07T00:00:00.000Z"), $"Ethereum 'date added' mismatch.");

            // Confirm the Ethereum platform is null
            Assert.That(ethereumInfo.Platform, Is.Null, $"Ethereum platform is not 'null'.");

            // Confirm that the currency has mineable tag associated with it
            Assert.That(ethereumInfo.Tags, Does.Contain("mineable"), $"Ethereum with ID {ethereumId} does not have the 'mineable' tag.");
        }

        /// <summary>
        /// Test to verify which of the first 10 cryptocurrencies are mineable.
        /// </summary>
        [Test]
        public async Task VerifyMineableCurrencies()
        {
            // Fetching info for currencies with IDs 1 through 10
            string ids = string.Join(",", Enumerable.Range(1, 10)); // Creates a comma-separated string "1,2,3,...,10"

            // Get the response from the API
            var cryptoInfoResponse = await GetApiResponse<CryptoInfoResponse>($"/v1/cryptocurrency/info?id={ids}", null);

            List<int> mineableCryptoIDs = new();

            // Checking for 'mineable' tag and printing out the relevant cryptocurrencies
            foreach (var kvp in cryptoInfoResponse.Data)
            {
                var cryptoData = kvp.Value; // Getting the CryptoData object

                // Confirm the crypto has the 'mineable' tag
                Assert.That(cryptoData.Tags, Does.Contain("mineable"), $"{cryptoData.Name} ({cryptoData.Symbol}) with ID {cryptoData.Id} does not have the 'mineable' tag.");

                if (cryptoData.Tags.Contains("mineable"))
                {
                    mineableCryptoIDs.Add(int.Parse(cryptoData.Id));
                    Console.WriteLine($"{cryptoData.Name} ({cryptoData.Symbol}) with ID {cryptoData.Id} is mineable.");
                }
            }

            // Convert the string of IDs to a list of integers for comparison
            var expectedIds = Enumerable.Range(1, 10).ToList();

            // Use CollectionAssert to confirm the collected list of IDs matches the expected list of IDs
            CollectionAssert.AreEquivalent(expectedIds, mineableCryptoIDs, "The list of mineable cryptocurrency IDs does not match the expected list.");
        }
    }
}
