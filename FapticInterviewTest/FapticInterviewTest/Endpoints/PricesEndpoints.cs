using FapticInterviewTest.Contracts.PricesResponse;
using FapticInterviewTest.Models;
using FapticInterviewTest.Services.BitfinexService;
using FapticInterviewTest.Services.BitStampService;
using FapticInterviewTest.Services.CRUDOperationsRepository;
using FapticInterviewTest.Services.PriceOperationsService;
using System.Diagnostics;

namespace FapticInterviewTest.Endpoints
{
    public static class PricesEndpoints
    {   
        /// <summary>
        /// Method used to map the Get endpoints in the Program.cs class 
        /// </summary>
        /// <param name="app"></param>
        public static void MapPricesEndpoints(this WebApplication app)
        {
            app.MapGet("/bitfinexcandles/trade={trade}&end={end}&start={start}&limit={limit}", GetBitfinexPrices);
            app.MapGet("/bitstampohlc/marketsymbol={marketsymbol}&end={end}&start={start}&limit={limit}&step={step}", GetBitStampPrices);
            app.MapGet("/aggregatedbitcoinprices/start={start}&end={end}&trade={trade}&marketsymbol={marketsymbol}&limit={limit}&step={step}", GetAggregatedBitcoinPrices);
            app.MapGet("/getaveragepricesfromdb/start={start}&end={end}", GetAveragePricesFromDb);
        }
        /// <summary>
        ///  Method used to add the register the Bitfinex service in Dependency Injection - Program.cs class
        /// </summary>
        /// <param name="service"></param>
        public static void AddBitfinexServices(this IServiceCollection service)
        {
            service.AddHttpClient<IBitfinexService, BitfinexService>();
        }
        /// <summary>
        ///  Method used to add the register the Bitstamp service in Dependency Injection - Program.cs class
        /// </summary>
        /// <param name="service"></param>
        public static void AddBitstampServices(this IServiceCollection service)
        {
            service.AddHttpClient<IBitstampService, BitStampService>();
        }

        /// <summary>
        /// Method used to add the register the PriceOperations service in Dependency Injection - Program.cs class
        /// </summary>
        /// <param name="service"></param>
        public static void AddPriceOperationsServices(this IServiceCollection service)
        {
            service.AddHttpClient<IPriceOperationsService, PriceOperationService>();
        }

        /// <summary>
        /// Method used to add the register the CRUD operations service in Dependency Injection - Program.cs class (repo pattern for data)
        /// </summary>
        /// <param name="service"></param>
        public static void AddCRUDOperationsServices(this IServiceCollection service)
        {
            service.AddHttpClient<ICRUDOperationsRepo, CRUDOperationsRepo>();
        }

        /// <summary>
        /// Method used to call the Bitfinex API -  sed just for me for testing to see how to parametrize the api calls
        /// </summary>
        /// <param name="priceOperationService"></param>
        /// <param name="service"></param>
        /// <param name="trade"></param>
        /// <param name="limit"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        internal static IResult GetBitfinexPrices(IPriceOperationsService priceOperationService, IBitfinexService service, string trade, int limit, DateTime start, DateTime end)
        {
            var startTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(start);
            var endTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(end);
            var bitfinexPricesList = service.GetBitfinexPricesAsync(trade, limit, startTimeString, endTimeString).Result;

            return bitfinexPricesList is not null ? Results.Ok(bitfinexPricesList) : Results.NotFound();
        }

        /// <summary>
        /// Method used to call the BitStamp API - used just for me for testing to see how to parametrize the api calls
        /// </summary>
        /// <param name="priceOperationService"></param>
        /// <param name="service"></param>
        /// <param name="marketSymbol"></param>
        /// <param name="limit"></param>
        /// <param name="step"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        internal static IResult GetBitStampPrices(IPriceOperationsService priceOperationService,IBitstampService service, string marketSymbol, int limit, int step, DateTime start, DateTime end)
        {
            var startTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(start);
            var endTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(end);

            var bitStampPricesList = service.GetBitStampOHLCPricesAsync(marketSymbol, limit, step, startTimeInt, endTimeInt).Result.data;

            return bitStampPricesList is not null ? Results.Ok(bitStampPricesList) : Results.NotFound();
        }

        /// <summary>
        /// Method used for aggregate endpoint (aggregate both price lists)
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="priceOperationService"></param>
        /// <param name="bitStampService"></param>
        /// <param name="bitfinexService"></param>
        /// <param name="marketSymbol"></param>
        /// <param name="trade"></param>
        /// <param name="limit"></param>
        /// <param name="step"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        internal static IResult GetAggregatedBitcoinPrices(ICRUDOperationsRepo crudService, IPriceOperationsService priceOperationService,IBitstampService bitStampService,IBitfinexService bitfinexService,string marketSymbol,string trade, int limit, int step, DateTime start, DateTime end)
        {
            //check if already we have in the database the price for the specific period of time
            var alreadyExistsPrice = crudService.AlreadyExistsInDB(start, end);   
            //if not
            if (alreadyExistsPrice == null)
            {   
                //convert from datetime to unix timestamp(seconds) for bitstamp api
                var bitStampStartTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(start);
                var bitStampEndTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(end);

                //convert from datetime to unix timestamp(milliseconds) for bitfinex api
                var bitfinexStartTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(start);
                var bitfinexEndTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(end);

                //call both api's in orther to get both price lists
                var bitStampPricesList = bitStampService.GetBitStampOHLCPricesAsync(marketSymbol, limit, step, bitStampStartTimeInt, bitStampEndTimeInt).Result;
                var bitfinexPricesList = bitfinexService.GetBitfinexPricesAsync(trade, limit, bitfinexStartTimeString, bitfinexEndTimeString).Result;
                
                //aggregate the lists with Querable.Average() method
                var aggregatedPrice = priceOperationService.GetAggregatedPricesAsync(bitStampPricesList, bitfinexPricesList);

                //insert the newly price in database 
                crudService.AddPriceInDB(start, end, aggregatedPrice);
                return Results.Ok(aggregatedPrice);
            }
                //default case if the price is in the database return it
                return Results.Ok(alreadyExistsPrice.AveragePrice);
        }

        internal static IResult GetAveragePricesFromDb(ICRUDOperationsRepo crudService,IPriceOperationsService priceService, DateTime start, DateTime end)
        {   
            //get the price list from db
            var averagePriceList = priceService.GetAveragePricesFromDb(crudService,start,end);

            //default case if the price is in the database return it
            return Results.Ok(averagePriceList);
        }
    }
}
