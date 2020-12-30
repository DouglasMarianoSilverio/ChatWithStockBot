using CWSB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CWSB.Core.Extensions
{
    public static class PostExtensions
    {
        const int command_prefix_lenght = 7;
        
        public static bool IsCommand(this Post post) => post.Text.StartsWith("/stock");

        public static string GetStockFromCommand(this Post post)
        {
            if (!post.IsCommand()) return null;            

            return post.Text.Remove(0, command_prefix_lenght);
        }
    }
}
