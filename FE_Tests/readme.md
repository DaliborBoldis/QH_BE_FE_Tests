# FE_Tests Project README

## Introduction

This project is set up for testing the front end services using NUnit with Selenium, designed with scalability in mind, primarily utilizing the NUnit framework for running tests.
Tests are structured to validate some front end elements on the coinmarketcap.com website. Tests are conducted through Visual Studio using the integrated Test Explorer.

## Prerequisites

Make sure you have the following installed:
- Visual Studio (with .NET Core support)
- NUnit Test Adapter
- Selenium

Before running the tests, make sure the Build Action for JavaScript files is set to "Embedded resource" under properties for each JS file.

## Packages

- NUnit (4.0.1)
- NUnit3TestAdapter (4.5.0)
- Selenium.WebDriver (4.18.1)
- Selenium.WebDriver.ChromeDriver (122.0.6261.6900)
- Microsoft.NET.Test.Sdk (17.5.0)
- MSTest.TestAdapter (2.2.10)
- MSTest.TestFramework (2.2.10)
- coverlet.collector (3.2.0)

Please verify that these packages are correctly restored in project. You can check the Dependencies in the Solution Explorer to confirm.

## Running Tests

To run tests in Visual Studio:

1. Open the Solution in Visual Studio.
2. Build the solution to ensure all dependencies are properly restored and the project compiles.
3. Open the Test Explorer by going to `Test > Test Explorer` from the main menu.
4. In the Test Explorer, you can run all tests or select individual tests to run.
5. Click on 'Run All' to execute all tests, or right-click on a specific test and choose 'Run' to execute a particular one.

## Test Structure

The tests are structured under the `Tests` directory. Each file is dedicated to a specific testing aspect:

- `HeatMap_Test.cs` (FE_TEST_1): Contains tests for verifying the heatmap functionality of the cryptocurrency interface, ensuring that the visual representation of data is correct and interactive elements within the heatmap are responsive.

- `Watchlist_Test.cs` (FE_TEST_2): Houses the tests for the watchlist feature, checking the ability to add and verify cryptocurrencies on a user's watchlist, simulating user interactions for watchlisting.

- `Filtering_Test.cs` (FE_TEST_3): Focused on testing the filtering capabilities of the UI, the test ensures that users can modify the rows amount in the table.

## Models

# Exceptions

The Exception directory is structured to hold the foundational classes for handling exceptions that the tests rely on.

- `Exceptions.cs`: Defines a suite of custom exceptions tailored to handle specific scenarios encountered during test execution. This includes not finding a cryptocurrency, issues with navigation or JavaScript execution, and problems with elements on the page.

# Pages

The Pages directory contains classes that correspond to pages within the application. Each class encapsulates the interactions and functionalities of a particular page:

- `BasePage.cs`: The cornerstone class that provides common web driver interactions and serves as the parent class for all other page classes.
- `FilteringPage.cs`: Represents the filtering interface of the application and contains methods specific to the operation of filtering data.
- `HeatMapPage.cs`: Encapsulates functionalities related to the cryptocurrency heat map page, such as navigation and interaction with heat map features.
- `WatchlistingPage.cs`: Manages the functionalities related to the cryptocurrency watchlist feature, including adding and verifying cryptos on a watchlist.

`Pages` directory is like the toolbox for our tests, each class a tool designed for specific elements and actions within the test suite.

## JavaScript Integration
JavaScript files are integrated as embedded resources and are crucial for the functionality of the tests. These scripts perform actions like fetching rows of data from a table, simulating user interactions, and scrolling through lazy-loaded content.

## Additional Notes
- For a detailed explanation of the JavaScript logic used in tests, refer to the scripts located in the `JavaScript` directory.
- Page-specific functionalities are encapsulated in corresponding classes within the `Pages` directory.
- Utilities such as the `JavaScriptLoader` and `WebDriverSetup` can be found in the `Utilities` directory.

## License

This project is licensed under the [MIT License](LICENSE).

Feel free to do anything with the code as long as you provide attribution back to me and don't hold me liable.
