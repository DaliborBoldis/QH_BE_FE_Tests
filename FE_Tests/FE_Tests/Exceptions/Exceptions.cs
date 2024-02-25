namespace CoinMarketCapTests.Exceptions
{
    /// <summary>
    /// Exception thrown when a specific cryptocurrency cannot be found.
    /// </summary>
    public class CryptoNotFoundException : Exception
    {
        public CryptoNotFoundException(string message)
            : base(message) { }
    }

    /// <summary>
    /// Exception thrown when navigation to a specific URL fails, possibly due to connectivity issues or incorrect URL.
    /// </summary>
    public class NavigationException : Exception
    {
        public NavigationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when opening a new browser tab fails, which could be due to browser or driver limitations.
    /// </summary>
    public class NewTabException : Exception
    {
        public NewTabException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when the execution of JavaScript on the page fails, which could be due to syntax errors or runtime errors in the script.
    /// </summary>
    public class JavaScriptExecutionException : Exception
    {
        // This constructor is used when we don't have an inner exception
        public JavaScriptExecutionException(string message)
            : base(message) { }

        // This constructor is used when we also have an inner exception
        public JavaScriptExecutionException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when loading a JavaScript resource fails, indicating the resource may be missing or inaccessible.
    /// </summary>
    public class JavaScriptLoadException : Exception
    {
        public JavaScriptLoadException(string message)
            : base(message) { }

        public JavaScriptLoadException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when an expected element on the page is not displayed, possibly due to dynamic content loading or rendering issues.
    /// </summary>
    public class ElementNotDisplayedException : Exception
    {
        public ElementNotDisplayedException(string message)
            : base(message) { }
    }

    /// <summary>
    /// Exception thrown when a specific element expected on the page is not found, potentially due to changes in the page structure or incorrect locators.
    /// </summary>
    public class ElementNotFoundException : Exception
    {
        // Existing constructor
        public ElementNotFoundException(string message)
            : base(message) { }

        // Add this constructor to accept an inner exception
        public ElementNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when multiple expected elements are not found on the page, suggesting a more significant issue with the page or its content.
    /// </summary>
    public class ElementsNotFoundException : Exception
    {
        // Constructor for a single message
        public ElementsNotFoundException(string message)
            : base(message) { }

        // Constructor to include an inner exception
        public ElementsNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when an element encountered does not match the expected criteria, which could be due to unexpected page changes or data.
    /// </summary>
    public class UnexpectedElementException : Exception
    {
        public UnexpectedElementException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
