﻿using Basic.Azure.Storage.Communications.Core;
using System;
using Basic.Azure.Storage.Communications.Core.Interfaces;

namespace Basic.Azure.Storage.Communications.BlobService.BlobOperations
{
    public class PutBlobResponse : IResponsePayload, IReceiveAdditionalHeadersWithResponse, IBlobPropertiesResponse
    {
        public string ETag { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime Date { get; set; }

        public string ContentMD5 { get; set; }


        public void ParseHeaders(System.Net.HttpWebResponse response)
        {
            //TODO: determine what we want to do about potential missing headers and date parsing errors

            ETag = response.Headers[ProtocolConstants.Headers.ETag].Trim(new char[] { '"' });
            Date = ParseDate(response.Headers[ProtocolConstants.Headers.OperationDate]);
            LastModified = ParseDate(response.Headers[ProtocolConstants.Headers.LastModified]);
            ContentMD5 = response.Headers[ProtocolConstants.Headers.ContentMD5];
        }

        private DateTime ParseDate(string headerValue)
        {
            DateTime dateValue;
            DateTime.TryParse(headerValue, out dateValue);
            return dateValue;
        }

    }
}
