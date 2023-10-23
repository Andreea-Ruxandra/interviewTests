using FapticInterviewTest.Services.BitStampService;

namespace FapticInterviewTest.Endpoints
{
    public static class BitStampEndpoints
    {

        public static void MapBitstampEndpoints(this WebApplication app)
        {
            app.MapGet("/bitstampohlc/marketSymbol={marketSymbol}&end={end}&start={start}&limit={limit}&step={step}", GetBitStampPrices);
        }

        public static void AddBitstampServices(this IServiceCollection service)
        {
            service.AddHttpClient<IBitstampService, BitStampService>();
        }

        internal static IResult GetBitStampPrices(IBitstampService service, string marketSymbol, int limit, int step, int start, int end)
        {
        
            var botStampPricesList = service.GetBitStampOHLCPricesAsync(marketSymbol,limit, step, start, end).Result.data;

            return botStampPricesList is not null ? Results.Ok(botStampPricesList) : Results.NotFound();
        }
    }
}
