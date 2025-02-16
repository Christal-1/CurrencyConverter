# Currency Converter

A simple .NET console application that converts currencies using the ExchangeRate-API.

## Features

- Convert currencies based on user input.
- Fetch real-time exchange rates from an API.
- Handle invalid inputs and errors gracefully.

## Requirements

- .NET 7.0 SDK
- An API key from [ExchangeRate-API](https://www.exchangerate-api.com/).

## Setup

1. Clone the repository:
    ```bash
    git clone https://github.com/Christal-1/CurrencyConverter.git
    ```
2. Navigate into the project directory:
    ```bash
    cd CurrencyConverter
    ```
3. Restore the project dependencies:
    ```bash
    dotnet restore
    ```
4. Run the application:
    ```bash
    dotnet run
    ```

## Configuration

Replace `YOUR_API_KEY` in `Program.cs` with your actual API key from [ExchangeRate-API](https://www.exchangerate-api.com/).

## Usage

1. Enter the base currency (e.g., USD).
2. Enter the target currency (e.g., EUR).
3. Enter the amount to convert.

