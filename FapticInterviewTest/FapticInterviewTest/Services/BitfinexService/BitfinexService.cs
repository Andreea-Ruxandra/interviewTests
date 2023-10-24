using FapticInterviewTest.Contracts.PricesResponse;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;

namespace FapticInterviewTest.Services.BitfinexService
{
    public class BitfinexService : IBitfinexService
    {
        private readonly HttpClient _httpClient;

        public BitfinexService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<List<double>>> GetBitfinexPricesAsync(string trade, int limit, string start, string end)
        {
            var uri = $"https://api-pub.bitfinex.com/v2/candles/{trade}/hist?start={start}&end={end}&limit={limit}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var bitStampPrices = JsonConvert.DeserializeObject<List<List<double>>>(responseString);
            return bitStampPrices;
        }
    }
}
