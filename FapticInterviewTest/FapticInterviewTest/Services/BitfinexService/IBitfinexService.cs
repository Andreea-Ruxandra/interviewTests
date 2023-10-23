using FapticInterviewTest.Contracts.BitStampResponse;

namespace FapticInterviewTest.Services.BitfinexService
{
    public interface IBitfinexService
    {
        public Task<List<List<double>>> GetBitfinexPricesAsync(string trade, int limit, string start, string end);
    }
}
