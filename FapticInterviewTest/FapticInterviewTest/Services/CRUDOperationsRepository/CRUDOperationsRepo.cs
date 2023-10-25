using FapticInterviewTest.Models;
using Microsoft.EntityFrameworkCore;

namespace FapticInterviewTest.Services.CRUDOperationsRepository
{   /// <summary>
   /// Implementation for the crud operations interface
  /// </summary>
    public class CRUDOperationsRepo : ICRUDOperationsRepo
    {
        private readonly PriceDbContext priceDbContext;
        private readonly HttpClient _httpClient;

        public CRUDOperationsRepo(PriceDbContext dbContext, HttpClient httpClient) {
            
            priceDbContext = dbContext;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Method used for inserting the price in the database for a specific period of time
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="aggregatedPrice"></param>

        public void AddPriceInDB(DateTime start, DateTime end, double aggregatedPrice)
        {
            //new price object created for inserting in DB with EF
            PriceModel price = new PriceModel();
            price.Start = start;
            price.End = end;
            price.AveragePrice = aggregatedPrice;

            //insert the price record and save changes via EF
            priceDbContext.Add(price);
            priceDbContext.SaveChanges();
        }
        /// <summary>
        /// Method used to check if the price is already in db for a specific period of time
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>

        public PriceModel AlreadyExistsInDB(DateTime start, DateTime end)
        {
            return priceDbContext.Prices.Where(x => x.Start == start && x.End == end).FirstOrDefault();
        }

        /// <summary>
        /// Method used for returning the prices in the database for a specific period of time, this method returns a list
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<PriceModel> GetPricesFromDbDuringTimeRange(DateTime start, DateTime end)
        {
            return priceDbContext.Prices.Where(x => x.Start == start && x.End == end).ToList();
        }

    }
}
