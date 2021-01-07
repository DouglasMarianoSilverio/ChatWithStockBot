

using CWSB.Core.Communications;

namespace StockBot.Model
{
    public class StockResponse
    {
        public string Symbol { get; set; }
        public string Close { get; set; }

        public bool Succeeded { get; set; }

        public ResponseResult ResponseResult { get; set; }
        
    }

}
