using CsvHelper;
using CWSB.Core.Communications;
using StockBot.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.Service
{
    public class StockService : IStockService
    {
        public StockService()
        {
        }

        public async Task<StockResponse> GetStock(string symbol)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://stooq.com/q/l/?s={symbol}&f=sd2t2ohlcv&h&e=csv");
                using (HttpResponseMessage response = await client.GetAsync(url) )
                {
                    if (!HandleErrorsResponse(response))
                    {
                        return new StockResponse
                        {
                            ResponseResult = new ResponseResult { }
                        };
                    }

                    var reader =  new StreamReader(response.Content.ReadAsStreamAsync().Result);

                    using (var csv = new CsvReader(reader, CultureInfo.CreateSpecificCulture("En-Us")))
                    {
                        var records = (List<StockResponse>)csv.GetRecords<StockResponse>();
                        return records[0];
                        
                    }
                }
            }
        }

        private bool HandleErrorsResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:                    
                case 400:
                    return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
