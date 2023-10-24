using FapticInterviewTest.Services.BitfinexService;
using FapticInterviewTest.Services.BitStampService;
using FapticInterviewTest.Services.PriceOperationsService;
using System.Diagnostics;

namespace FapticInterviewTest.Endpoints
{
    public static class PricesEndpoints
    {
        public static void MapPricesEndpoints(this WebApplication app)
        {
            app.MapGet("/bitfinexcandles/trade={trade}&end={end}&start={start}&limit={limit}", GetBitfinexPrices);
            app.MapGet("/bitstampohlc/marketsymbol={marketsymbol}&end={end}&start={start}&limit={limit}&step={step}", GetBitStampPrices);
            app.MapGet("/aggregatedbitcoinprices/start={start}&end={end}&trade={trade}&marketsymbol={marketsymbol}&limit={limit}&step={step}", GetAggregatedBitcoinPrices);
        }

        public static void AddBitfinexServices(this IServiceCollection service)
        {
            service.AddHttpClient<IBitfinexService, BitfinexService>();
        }

        public static void AddBitstampServices(this IServiceCollection service)
        {
            service.AddHttpClient<IBitstampService, BitStampService>();
        }

        public static void AddPriceOperationsServices(this IServiceCollection service)
        {
            service.AddHttpClient<IPriceOperationsService, PriceOperationService>();
        }

        internal static IResult GetBitfinexPrices(IPriceOperationsService priceOperationService, IBitfinexService service, string trade, int limit, DateTime start, DateTime end)
        {
            var startTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(start);
            var endTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(end);
            var bitfinexPricesList = service.GetBitfinexPricesAsync(trade, limit, startTimeString, endTimeString).Result;

            return bitfinexPricesList is not null ? Results.Ok(bitfinexPricesList) : Results.NotFound();
        }

        internal static IResult GetBitStampPrices(IPriceOperationsService priceOperationService,IBitstampService service, string marketSymbol, int limit, int step, DateTime start, DateTime end)
        {
            var startTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(start);
            var endTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(end);

            var bitStampPricesList = service.GetBitStampOHLCPricesAsync(marketSymbol, limit, step, startTimeInt, endTimeInt).Result.data;

            return bitStampPricesList is not null ? Results.Ok(bitStampPricesList) : Results.NotFound();
        }

        internal static IResult GetAggregatedBitcoinPrices(IPriceOperationsService priceOperationService,IBitstampService bitStampService,IBitfinexService bitfinexService,string marketSymbol,string trade, int limit, int step, DateTime start, DateTime end)
        {
            var bitStampStartTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(start);
            var bitStampEndTimeInt = priceOperationService.GetUnixTimestampSecFromDateTime(end);

            var bitfinexStartTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(start);
            var bitfinexEndTimeString = priceOperationService.GetUnixTimestampMilliFromDateTime(end);

            var bitStampPricesList = bitStampService.GetBitStampOHLCPricesAsync(marketSymbol, limit, step, bitStampStartTimeInt, bitStampEndTimeInt).Result;
            var bitfinexPricesList = bitfinexService.GetBitfinexPricesAsync(trade, limit, bitfinexStartTimeString, bitfinexEndTimeString).Result;
            var aggregatedPriceList = priceOperationService.GetAggregatedPricesAsync(bitStampPricesList, bitfinexPricesList);

            return Results.Ok(aggregatedPriceList);
        }
    }
}
