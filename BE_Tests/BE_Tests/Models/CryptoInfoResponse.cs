namespace BE_Tests.Models
{
    /// <summary>
    /// Represents the response containing cryptocurrency information.
    /// </summary>
    public class CryptoInfoResponse
    {
        /// <summary>
        /// A collection of cryptocurrency data keyed by their unique identifiers.
        /// </summary>
        public Dictionary<string, CryptoData> Data { get; set; } = new Dictionary<string, CryptoData>();
    }

    /// <summary>
    /// Contains detailed information about a cryptocurrency.
    /// </summary>
    public class CryptoData
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Date_added { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public object Platform { get; set; } = new object(); // The platform can be null or have data, hence 'object' type.
        public Urls Urls { get; set; } = new Urls();
    }

    /// <summary>
    /// Represents various URLs related to a cryptocurrency, like its website, technical documentation, etc.
    /// </summary>
    public class Urls
    {
        public List<string> Website { get; set; } = new List<string>();
        public List<string> Technical_doc { get; set; } = new List<string>();
        // Include other URL types as needed
    }
}