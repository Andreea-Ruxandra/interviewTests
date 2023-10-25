using FapticInterviewTest.Contracts.PricesResponse;
using FapticInterviewTest.Models;
using FapticInterviewTest.Services.BitfinexService;
using FapticInterviewTest.Services.BitStampService;
using FapticInterviewTest.Services.CRUDOperationsRepository;

namespace FapticInterviewTest.Services.PriceOperationsService
{
    /// <summary>
    /// Interface for price operations service which contains the signature definition for the methods which will be implemented in the service
    /// This was made for the timestamp conversions and the aggregation for the price lists and other futher operations
    /// </summary>
    public interface IPriceOperationsService
    {
        public double GetAggregatedPricesAsync(Root bitstampPrices, List<List<double>> bitfinexPrices);
        public int GetUnixTimestampSecFromDateTime(DateTime period);
        public string GetUnixTimestampMilliFromDateTime(DateTime period);
        public List<PriceModel> GetAveragePricesFromDb(ICRUDOperationsRepo crudService, DateTime start, DateTime end);
    }
}
