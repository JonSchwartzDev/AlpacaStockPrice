using Alpaca.Markets;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace CodeExamples
{
    internal static class Example
    {
        private static string API_KEY = "dfasdfasdfsdfdas";

        private static string API_SECRET = "sdfasdfasdfasdfdasfasdfdsfsdfewrfdvcasdfas";

        public static async Task Main(string[] args)
        {
            // First, open the API connection
            var client = Alpaca.Markets.Environments.Paper
                .GetAlpacaDataClient(new SecretKey(API_KEY, API_SECRET));

            Write("Enter the symbol to check: ");
            var symbol = ReadLine();

            while (symbol.Trim().Length > 0)
            {
                Write("Enter the number of days to check percent change over: ");
                double days = double.Parse(ReadLine());

                var into = DateTime.Today;
                var from = into.AddDays(days * -1);

                var bars = await client.ListHistoricalBarsAsync(
                    new HistoricalBarsRequest(symbol, from, into, BarTimeFrame.Day));

                var startPrice = bars.Items.First().Open;
                var endPrice = bars.Items.Last().Close;

                var percentChange = (endPrice - startPrice) / startPrice;
                Console.WriteLine($"{symbol} moved {percentChange:P} over the last {days} days.");
                Console.WriteLine();

                Write("Enter the symbol to check, or blank to exit: ");
                symbol = ReadLine();
            }
        }
    }
}