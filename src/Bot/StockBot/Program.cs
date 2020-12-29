using StockBot.Service;
using System;
using System.Threading.Tasks;

namespace StockBot
{
    class Program
    {
        static void Main(string[] args)
        {
            IStockService service = new StockService();
            var x = Task.FromResult(service.GetStock(""));
        }
    }
}
