namespace BE_Tests.Models
{
    /// <summary>
    /// Represents the response from a request for cryptocurrency data.
    /// </summary>
    public class CryptoResponse
    {
        /// <summary>
        /// The status of the response, including any error codes or messages.
        /// </summary>
        public Status Status { get; set; } = new Status();

        /// <summary>
        /// A list of cryptocurrency data.
        /// </summary>
        public List<CryptoData> Data { get; set; } = new List<CryptoData>();
    }

    /// <summary>
    /// Represents the status details of an API response, including timestamps, error codes, and messages.
    /// </summary>
    public class Status
    {
        public string Timestamp { get; set; } = string.Empty;
        public int Error_code { get; set; }
        public string Error_message { get; set; } = string.Empty;

        public int StatusCode { get; set; }
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
