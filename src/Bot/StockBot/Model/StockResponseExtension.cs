namespace StockBot.Model
{
    public static class StockResponseExtension
    {
        public static string GetStockQuoteFormatted(this StockResponse stock)
        {
            if (!stock.Succeeded)
                return $"Cannot fetch information for {stock.Symbol}";
            else if ( string.IsNullOrWhiteSpace(stock.Close) ||  stock.Close.Equals("N/D"))
                return $"{stock.Symbol} quote is not recognized";
            return $"{stock.Symbol} quote is { stock.Close} per share";
            
        }
    }
}
