using CWSB.Core.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Models
{
    public class PostCreateResponse
    {
        public bool Succeeded { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
