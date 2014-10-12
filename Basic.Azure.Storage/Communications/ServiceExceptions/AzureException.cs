﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Azure.Storage.Communications.ServiceExceptions
{
    public class AzureException : Exception
    {
        public string RequestId { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string StatusDescription { get; private set; }
        public Dictionary<string, string> Details { get; private set; }

        public AzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(String.Format("{0}. Request {1}, Http Status: {2}\n{3}", statusDescription, requestId, (int)statusCode, string.Join(", ", details.Select(kvp => String.Format("{0}: {1}", kvp.Key, kvp.Value)))), baseException)
        {
            RequestId = requestId;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Details = details;
        }

    }
}
