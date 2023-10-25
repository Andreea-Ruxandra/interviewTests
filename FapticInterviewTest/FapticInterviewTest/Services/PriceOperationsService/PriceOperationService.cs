using FapticInterviewTest.Contracts.PricesResponse;
using FapticInterviewTest.Models;
using FapticInterviewTest.Services.CRUDOperationsRepository;
using System.Linq;

namespace FapticInterviewTest.Services.PriceOperationsService
{  /// <summary>
   /// PriceOperation service implementation
   /// </summary>
    public class PriceOperationService : IPriceOperationsService
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Http client initialization 
        /// </summary>
        /// <param name="httpClient"></param>
        public PriceOperationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// Method used for aggregate both price lists from both sources: Bitfinex and Bitstamp, it return the aggregated price
        /// </summary>
        /// <param name="bitstampPrices"></param>
        /// <param name="bitfinexPrices"></param>
        /// <returns></returns>
        public double GetAggregatedPricesAsync(Root bitstampPrices, List<List<double>> bitfinexPrices)
        {   
            //new blank list for aggregated prices
            List<double> parsedPrices = new List<double>();

            //variable used for the tryparse statement(from string close price to double)
            double result;

            //get every item from the list (Bitstamp)
            foreach (var bitstampItem in bitstampPrices.data.ohlc)
            {  
                //convert the string price to double
                var converted = double.TryParse(bitstampItem.close, out result);
                //add it in the newly created list
                parsedPrices.Add(result);
            }

            //get every item from the list (Bitfinex)
            foreach (var bitfinexItem in bitfinexPrices)
            {   
                //here we dont have a label in the json list so we get the second element from the list (candle definition in swagger)
                var closePrice = bitfinexItem.ToArray<double>().ElementAt(2);
                
                //add it in the newly created list
                parsedPrices.Add(closePrice);
            }

            //Queryable average method used on the combined price list and return the average price
            return Queryable.Average(parsedPrices.AsQueryable());

        }

        /// <summary>
        /// Method used to convert from DateTime to Unix timestamp in seconds
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <returns></returns>
        public int GetUnixTimestampSecFromDateTime(DateTime timePeriod)
        {   
            //convertion statement: convert the offset of timePeriod to unixtime
            var timePeriodUnixTime = ((DateTimeOffset)timePeriod).ToUnixTimeSeconds().ToString();
            int timePeriodInt;

            //the final result should be timePeriodInt which needs to be of type int for calling the API
            var result = int.TryParse(timePeriodUnixTime, out timePeriodInt);

            return timePeriodInt;
        }
        /// <summary>
        /// Method used to convert from DateTime to Unix timestamp in milliseconds
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public string GetUnixTimestampMilliFromDateTime(DateTime period)
        {   //convertion statement: convert the offset of period in unixtime milliseconds and then applying ToString(), because the result should be a string for calling the API
            var timePeriodUnixTime = ((DateTimeOffset)period).ToUnixTimeMilliseconds().ToString();

            return timePeriodUnixTime;
        }

        public List<PriceModel> GetAveragePricesFromDb(ICRUDOperationsRepo crudService, DateTime start, DateTime end)
        {
            //check if already we have in the database the price for the specific period of time
            var alreadyExistsPrice = crudService.GetPricesFromDbDuringTimeRange(start, end);
            return alreadyExistsPrice;
        }
    }
}
