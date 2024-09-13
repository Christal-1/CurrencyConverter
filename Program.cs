// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    class Program
    {
        private static readonly string apiKey = "b5c693ff82dd7a6ebef49571"; // Insert your API key here
        private static readonly string apiUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Currency Converter!");

            // Get user input for the currencies
            Console.Write("Enter the base currency (e.g., USD): ");
            string? baseCurrency = Console.ReadLine()?.ToUpper();

            Console.Write("Enter the target currency (e.g., EUR): ");
            string? targetCurrency = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(baseCurrency) || string.IsNullOrWhiteSpace(targetCurrency))
            {
                Console.WriteLine("Invalid currency input. Exiting...");
                return;
            }

            Console.Write("Enter the amount to convert: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount. Exiting...");
                return;
            }

            // Fetch exchange rates
            decimal exchangeRate = await GetExchangeRate(baseCurrency, targetCurrency);

            if (exchangeRate > 0)
            {
                decimal convertedAmount = amount * exchangeRate;
                Console.WriteLine($"{amount} {baseCurrency} is equal to {convertedAmount} {targetCurrency}");
            }
            else
            {
                Console.WriteLine("Could not retrieve exchange rate. Please try again later.");
            }
        }

        // Method to fetch exchange rate
        private static async Task<decimal> GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string requestUrl = $"{apiUrl}{baseCurrency}";
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(jsonResponse);
                        
                        // Check if the target currency exists in the response
                        if (data["conversion_rates"]?[targetCurrency] != null)
                        {
                            decimal exchangeRate = data["conversion_rates"][targetCurrency].Value<decimal>();
                            return exchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("Target currency not found in response.");
                            return -1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error fetching exchange rates.");
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return -1;
                }
            }
        }
    }
}
