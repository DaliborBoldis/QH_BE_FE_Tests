using System.IO;
using System.Reflection;
using CoinMarketCapTests.Exceptions;

namespace CoinMarketCapTests.Utilities
{
    public static class JavaScriptLoader
    {
        /// <summary>
        /// Loads up the JavaScript script by its name.
        /// Finds the script in resources, and reads it.
        /// </summary>
        /// <param name="scriptName">The name of the JavaScript file.</param>
        /// <returns>The script's content, ready to be executed.</returns>
        public static string Load(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = $"FE_Tests.JavaScript.{scriptName}.js";

            using Stream stream = assembly.GetManifestResourceStream(resourceName) ?? throw new JavaScriptLoadException($"The JavaScript resource '{resourceName}' could not be loaded because it was not found.");

            using StreamReader reader = new(stream);

            // Return the script content
            return reader.ReadToEnd();
        }
    }
}
