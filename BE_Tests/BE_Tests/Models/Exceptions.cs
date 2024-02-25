using System.Net;

namespace BE_Tests.Models
{
    /// <summary>
    /// Represents errors that occur during the deserialization process.
    /// </summary>
    public class DeserializationException : Exception
    {
        public DeserializationException() { }

        public DeserializationException(string message)
            : base(message) { }

        public DeserializationException(string message, Exception inner)
            : base(message, inner) { }
    }

    /// <summary>
    /// Represents errors related to API response, including the status code.
    /// </summary>
    public class ApiResponseException : Exception
    {
        /// <summary>
        /// The HTTP status code associated with the API error.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public ApiResponseException() { }

        public ApiResponseException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiResponseException(string message, HttpStatusCode statusCode, Exception inner)
            : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}