﻿using CWSB.WebApp.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleErrorsResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode) 
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpResponseException(response.StatusCode);
                case 400:
                    return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
