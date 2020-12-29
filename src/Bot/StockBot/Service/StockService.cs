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


//using (HttpResponseMessage response = Client.GetAsync($"https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv").Result)
//using (HttpContent content = response.Content)
//{
//    var callResponse = content.ReadAsStringAsync().Result;
//    if (response.StatusCode != System.Net.HttpStatusCode.OK)
//        throw new ArgumentException(callResponse);
//    var data = callResponse.Substring(callResponse.IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
//    var processedArray = data.Split(',');
//    return new StockResponse()
//    {
//        Symbol = processedArray[0],
//        Date = !processedArray[1].Contains("N/D") ? Convert.ToDateTime(processedArray[1]) : default,
//        Time = !processedArray[2].Contains("N/D") ? Convert.ToDateTime(processedArray[2]).TimeOfDay : default,
//        Open = !processedArray[3].Contains("N/D") ? Convert.ToDouble(processedArray[3]) : default,
//        High = !processedArray[4].Contains("N/D") ? Convert.ToDouble(processedArray[4]) : default,
//        Low = !processedArray[5].Contains("N/D") ? Convert.ToDouble(processedArray[5]) : default,
//        Close = !processedArray[6].Contains("N/D") ? Convert.ToDouble(processedArray[6]) : default,
//        Volume = !processedArray[7].Contains("N/D") ? Convert.ToDouble(processedArray[7]) : default,
//    };
//}
