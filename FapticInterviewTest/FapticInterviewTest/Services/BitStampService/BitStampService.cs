using FapticInterviewTest.Contracts.PricesResponse;
using Newtonsoft.Json;

namespace FapticInterviewTest.Services.BitStampService
{  /// <summary>
   /// Bitstamp service implementation
   /// </summary>
    public class BitStampService : IBitstampService
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Http client initialization 
        /// </summary>
        /// <param name="httpClient"></param>
        public BitStampService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// Get Api call to the external Bitstamp api (async + await)
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="limit"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<Root> GetBitStampOHLCPricesAsync(string marketSymbol, int limit, int step, int start, int end)
        {
            //url parametrized from query string
            var uri = $"https://www.bitstamp.net/api/v2/ohlc/{marketSymbol}/?step={step}&limit={limit}&start={start} &end={end}";

            //waiting for the response to be materialized
            var responseString = await _httpClient.GetStringAsync(uri);

            //deserialize the response by the type Root
            var bitStampPrices = JsonConvert.DeserializeObject<Root>(responseString);

            return bitStampPrices;
        }

    }
}
