using FapticInterviewTest.Services.BitfinexService;
using FapticInterviewTest.Services.BitStampService;

namespace FapticInterviewTest.Endpoints
{
    public static class BitfinexEndpoints
    {
        public static void MapBitfinexEndpoints(this WebApplication app)
        {
            app.MapGet("/bitfinexcandles/trade={trade}&end={end}&start={start}&limit={limit}", GetBitfinexPrices);
        }

        public static void AddBitfinexServices(this IServiceCollection service)
        {
            service.AddHttpClient<IBitfinexService, BitfinexService>();
        }

        internal static IResult GetBitfinexPrices(IBitfinexService service, string trade, int limit, string start, string end)
        {

            var botStampPricesList = service.GetBitfinexPricesAsync(trade, limit, start, end).Result;

            return botStampPricesList is not null ? Results.Ok(botStampPricesList) : Results.NotFound();
        }
    }
}
