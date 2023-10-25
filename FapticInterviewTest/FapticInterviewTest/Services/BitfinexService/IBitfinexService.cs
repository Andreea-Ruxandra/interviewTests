namespace FapticInterviewTest.Services.BitfinexService
{   
    /// <summary>
    /// Interface for Bitfinex service which contains the signature definition for the methods which will be implemented in the service
    /// </summary>
    public interface IBitfinexService
    {
        public Task<List<List<double>>> GetBitfinexPricesAsync(string trade, int limit, string start, string end);
    }
}
