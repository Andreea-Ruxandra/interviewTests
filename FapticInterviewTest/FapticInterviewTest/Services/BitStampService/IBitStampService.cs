using FapticInterviewTest.Contracts.BitStampResponse;

namespace FapticInterviewTest.Services.BitStampService
{
    public interface IBitstampService
    {
        public Task<Root> GetBitStampOHLCPricesAsync(string marketSymbol, int limit, int step, int start, int end);
    }
}
