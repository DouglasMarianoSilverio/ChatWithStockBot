using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public  int ExpiresInHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
}
