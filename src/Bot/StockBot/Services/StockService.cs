using CsvHelper;
using CWSB.Core.Communications;
using StockBot.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.Services
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

                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (!HandleErrorsResponse(response))
                    {
                        return new StockResponse
                        {
                            Succeeded = false,
                            Symbol = symbol,
                            ResponseResult = new ResponseResult { }
                        };
                    }

                    using (HttpContent content = response.Content)
                    using (var stream = (MemoryStream)await content.ReadAsStreamAsync())
                    using (var sr = new StreamReader(stream))
                    using (var csvReader = new CsvReader(sr, CultureInfo.InvariantCulture))
                    {
                        csvReader.Configuration.Delimiter = ",";
                        csvReader.Configuration.IgnoreBlankLines = true;
                        csvReader.Configuration.HasHeaderRecord = true;
                        csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                        csvReader.Configuration.MissingFieldFound = null;
                        csvReader.Read();
                        csvReader.ReadHeader();
                        var record = csvReader.GetRecords<StockResponse>().ToList().FirstOrDefault();
                        record.Succeeded = true;
                        return record;
                    };
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
