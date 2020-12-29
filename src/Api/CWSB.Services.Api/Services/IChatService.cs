﻿using CWSB.Services.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Services
{
    public interface IPostService
    {
        Task<PostCreateResponse> PostMessage(Post post);
    }
}
