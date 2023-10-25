using FapticInterviewTest.Models;

namespace FapticInterviewTest.Services.CRUDOperationsRepository
{   /// <summary>
/// Interface for crud operations service which contains the signature definition for the methods which will be implemented in the service
/// </summary>
    public interface ICRUDOperationsRepo
    {
        public PriceModel AlreadyExistsInDB(DateTime start, DateTime end);
        public List<PriceModel> GetPricesFromDbDuringTimeRange(DateTime start, DateTime end);
        public void AddPriceInDB(DateTime start, DateTime end, double aggregatedPrice);

    }
}
