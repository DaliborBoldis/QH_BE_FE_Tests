namespace BE_Tests.Models
{
    /// <summary>
    /// Represents the data required for currency conversion. Tightly linked to the Quote and BOB classes.
    /// Note: These classes are simple now but consider separation if they evolve.
    /// </summary>
    public class ConversionData
    {
        /// <summary>
        /// The quote associated with the conversion.
        /// </summary>
        public Quote Quote { get; set; } = new Quote();
    }

    /// <summary>
    /// Represents a quote in the context of currency conversion.
    /// </summary>
    public class Quote
    {
        /// <summary>
        /// The BOB (Boliviano) conversion details.
        /// </summary>
        public BOB BOB { get; set; } = new BOB();
    }

    /// <summary>
    /// Represents the Boliviano currency details.
    /// </summary>
    public class BOB
    {
        /// <summary>
        /// The price in BOB.
        /// </summary>
        public double Price { get; set; } = new double();
    }
}
