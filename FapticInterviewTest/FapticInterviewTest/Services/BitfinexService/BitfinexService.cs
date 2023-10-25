using FapticInterviewTest.Contracts.PricesResponse;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;

namespace FapticInterviewTest.Services.BitfinexService
{   /// <summary>
    /// Bitfinex service implementation
    /// </summary>
    public class BitfinexService : IBitfinexService
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Http client initialization 
        /// </summary>
        /// <param name="httpClient"></param>
        public BitfinexService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// Get Api call to the external Bitfinex api (async + await)
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="limit"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<List<double>>> GetBitfinexPricesAsync(string trade, int limit, string start, string end)
        {   
            //url parametrized from query string
            var uri = $"https://api-pub.bitfinex.com/v2/candles/{trade}/hist?start={start}&end={end}&limit={limit}";
            
            //waiting for the response to be materialized
            var responseString = await _httpClient.GetStringAsync(uri);

            //deserialize the response by the type List<List<double>>
            var bitStampPrices = JsonConvert.DeserializeObject<List<List<double>>>(responseString);

            return bitStampPrices;
        }
    }
}
