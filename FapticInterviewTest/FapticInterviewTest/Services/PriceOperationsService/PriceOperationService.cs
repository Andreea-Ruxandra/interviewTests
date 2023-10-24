using FapticInterviewTest.Contracts.PricesResponse;
using System.Linq;

namespace FapticInterviewTest.Services.PriceOperationsService
{
    public class PriceOperationService : IPriceOperationsService
    {
        private readonly HttpClient _httpClient;

        public PriceOperationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
       
        public double GetAggregatedPricesAsync(Root bitstampPrices, List<List<double>> bitfinexPrices)
        {
            List<double> parsedPrices = new List<double>();
            double result;
            foreach (var bitstampItem in bitstampPrices.data.ohlc)
            {
                var converted = double.TryParse(bitstampItem.close, out result);
                parsedPrices.Add(result);
            }

            foreach (var bitfinexItem in bitfinexPrices)
            {
                var closePrice = bitfinexItem.ToArray<double>().ElementAt(2);
                parsedPrices.Add(closePrice);
            }

            return Queryable.Average(parsedPrices.AsQueryable());

        }

        public int GetUnixTimestampSecFromDateTime(DateTime timePeriod)
        {
            var timePeriodUnixTime = ((DateTimeOffset)timePeriod).ToUnixTimeSeconds().ToString();
            int timePeriodInt;
            var result = int.TryParse(timePeriodUnixTime, out timePeriodInt);
            return timePeriodInt;
        }

        public string GetUnixTimestampMilliFromDateTime(DateTime period)
        {
            var timePeriodUnixTime = ((DateTimeOffset)period).ToUnixTimeMilliseconds().ToString();
            return timePeriodUnixTime;
        }
    }
}
