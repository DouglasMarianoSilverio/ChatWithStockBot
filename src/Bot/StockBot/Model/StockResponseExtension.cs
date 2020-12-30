using System;
using System.Collections.Generic;
using System.Text;

namespace StockBot.Model
{
    public static class StockResponseExtension
    {
        public static string GetStockQuoteFormatted(this StockResponse stock)
        {
            return $"{stock.Symbol} quote is ${stock.Close} per share";
        }
    }
}
