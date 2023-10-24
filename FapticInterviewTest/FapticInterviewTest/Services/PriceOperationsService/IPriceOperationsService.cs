using FapticInterviewTest.Contracts.PricesResponse;
using FapticInterviewTest.Services.BitfinexService;
using FapticInterviewTest.Services.BitStampService;

namespace FapticInterviewTest.Services.PriceOperationsService
{
    public interface IPriceOperationsService
    {
        public double GetAggregatedPricesAsync(Root bitstampPrices, List<List<double>> bitfinexPrices);
        public int GetUnixTimestampSecFromDateTime(DateTime period);
        public string GetUnixTimestampMilliFromDateTime(DateTime period);
    }
}
