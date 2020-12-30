using CWSB.Core.Extensions;
using CWSB.Core.Models;
using Xunit;

namespace CWSB.Core.Tests.Extensions
{
    public class StockExtensionsTest
    {
        [Fact]
        public void ExtractStockCodeFromMessage()
        {
            var stockExpected = "xp.us";
            Post post = new Post("/stock=xp.us","testeUser");

            var stockCode = post.GetStockFromCommand();

            Assert.Equal(stockExpected, stockCode);
        }

        [Fact]
        public void ConfirmThatMessageIsACommand()
        {            
            Post post = new Post("/stock=xp.us", "testeUser");           
            Assert.True(post.IsCommand()); 
        }

        [Fact]
        public void ConfirmThatMessageIsNotACommand()
        {
            Post post = new Post("do you know current value for xp.us?", "testeUser");
            Assert.False(post.IsCommand());
        }
    }
}
