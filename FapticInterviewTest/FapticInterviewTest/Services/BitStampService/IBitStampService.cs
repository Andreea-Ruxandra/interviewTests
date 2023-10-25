using FapticInterviewTest.Contracts.PricesResponse;

namespace FapticInterviewTest.Services.BitStampService
{   /// <summary>
    /// Interface for Bitstamp service which contains the signature definition for the methods which will be implemented in the service
    /// </summary>
    public interface IBitstampService
    {
        public Task<Root> GetBitStampOHLCPricesAsync(string marketSymbol, int limit, int step, int start, int end);
    }
}
