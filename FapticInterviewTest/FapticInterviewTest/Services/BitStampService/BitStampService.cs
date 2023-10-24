using FapticInterviewTest.Contracts.PricesResponse;
using Newtonsoft.Json;

namespace FapticInterviewTest.Services.BitStampService
{
    public class BitStampService : IBitstampService
    {
        private readonly HttpClient _httpClient;

        public BitStampService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Root> GetBitStampOHLCPricesAsync(string marketSymbol, int limit, int step, int start, int end)
        {

            var uri = $"https://www.bitstamp.net/api/v2/ohlc/{marketSymbol}/?step={step}&limit={limit}&start={start} &end={end}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var bitStampPrices = JsonConvert.DeserializeObject<Root>(responseString);

            return bitStampPrices;
        }

    }
}
