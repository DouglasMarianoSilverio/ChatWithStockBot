using StockBot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.Service
{
    public interface IStockService
    {
        Task<StockResponse> GetStock(string symbol);
    }
}
