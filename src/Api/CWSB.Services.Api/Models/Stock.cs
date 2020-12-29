namespace CWSB.Services.Api.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Close { get; set; }
        public string StockInfo()
        {
            return $"{Symbol} quote is ${Close} per share";
        }
    }

}
