﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Extensions
{
    public class CustomHttpResponseException : Exception
    {

        public HttpStatusCode StatusCode;

        public CustomHttpResponseException()
        {
        }

        public CustomHttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public CustomHttpResponseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
