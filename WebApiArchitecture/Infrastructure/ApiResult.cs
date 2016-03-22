using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApiArchitecture.Infrastructure
{
    public class ApiResult
    {
        public string Version { get { return "1.0.0"; } }

        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        public ApiResult(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}