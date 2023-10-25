using Newtonsoft.Json;

namespace FapticInterviewTest.Contracts.PricesResponse
{   /// <summary>
    /// Bitstamp json response structure class - price structure - response we get from bitstamp api
    /// </summary>
    public class Data
    {
        public List<Ohlc> ohlc { get; set; } = new List<Ohlc>();
        public string pair { get; set; }
    }

    public class Ohlc
    {   

        /// <summary>
        /// The price we are interested is the close price
        /// </summary>
        public string close { get; set; }

        
        //public string high { get; set; }

      
        //public string low { get; set; }

       
        //public string open { get; set; }

       
        //public string timestamp { get; set; }

        
        //public string volume { get; set; }
    }
    public class Root
    {
        public Data data { get; set; }
    }

}
