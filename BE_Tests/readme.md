# BE_Tests Project README

## Introduction

This project is set up for testing backend services using NUnit with RestAssured.Net. Tests are structured to validate currency conversions and cryptocurrency information retrieval.

## Prerequisites

Make sure you have the following installed:

- Visual Studio (with .NET Core support)
- NUnit Test Adapter
- RestAssured.Net

## Packages

The required packages are listed below and are included in the project's dependencies:

- NUnit (4.0.1)
- NUnit3TestAdapter (4.5.0)
- RestAssured.Net (4.2.1)
- Microsoft.NET.Test.Sdk (17.5.0)
- coverlet.collector (3.2.0)
- MSTest.TestAdapter (2.2.10)
- MSTest.TestFramework (2.2.10)
- Newtonsoft.Json (13.0.3)

Please verify that these packages are correctly restored in your project. You can check the Dependencies in the Solution Explorer to confirm.

## Running Tests

To run tests in Visual Studio:

1. Open the Solution in Visual Studio.
2. Build the solution to ensure all dependencies are properly restored and the project compiles.
3. Open the Test Explorer by going to `Test > Test Explorer` from the main menu.
4. In the Test Explorer, you can run all tests or select individual tests to run.
5. Click on 'Run All' to execute all tests, or right-click on a specific test and choose 'Run' to execute a particular one.

Test results will be displayed within the Test Explorer, giving you a detailed report of passed and failed tests.

## Test Structure

The tests are structured under the `Tests` directory. Here's a brief overview of what file contains:

- `Tests.cs`: Contains tests for cryptocurrency data retrieval and conversion using the CoinMarketCap API.

## Models

The Models directory contains classes that represent the data required for tests:

- `ConversionData.cs`: Represents the data required for currency conversion.
- `ConversionResponse.cs`: Represents the response received after a conversion operation.
- `CryptoInfoResponse.cs`: Represents the response containing cryptocurrency information.
- `CryptoResponse.cs`: Represents the response from a request for cryptocurrency data.
- `Exceptions.cs`: Contains custom exceptions for handling API response errors.

These models are important for deserialization of the JSON responses from the API and are used within the tests to validate response data.

## License

This project is licensed under the [MIT License](LICENSE).

Feel free to do anything with the code as long as you provide attribution back to me and don't hold me liable.
