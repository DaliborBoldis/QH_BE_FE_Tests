namespace BE_Tests.Models
{
    /// <summary>
    /// Represents the response received after a conversion operation.
    /// </summary>
    public class ConversionResponse
    {
        /// <summary>
        /// The status of the conversion operation.
        /// </summary>
        public Status Status { get; set; } = new Status();

        /// <summary>
        /// The data resulting from the conversion operation.
        /// </summary>
        public ConversionData Data { get; set; } = new ConversionData();
    }
}
