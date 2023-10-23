using Newtonsoft.Json;

namespace FapticInterviewTest.Contracts.BitStampResponse
{
    public class Data
    {
        public List<Ohlc> ohlc { get; set; } = new List<Ohlc>();
        public string pair { get; set; }
    }

    public class Ohlc
    {
        [JsonProperty("close")]
        public string close { get; set; }

        [JsonProperty("high")]
        public string high { get; set; }

        [JsonProperty("low")]
        public string low { get; set; }

        [JsonProperty("open")]
        public string open { get; set; }

        [JsonProperty("timestamp")]
        public string timestamp { get; set; }

        [JsonProperty("volume")]
        public string volume { get; set; }
    }
    public class Root
    {
        public Data data { get; set; }
    }

}
