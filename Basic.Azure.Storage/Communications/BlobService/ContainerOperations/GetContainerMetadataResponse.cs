﻿using Basic.Azure.Storage.Communications.Common;
using Basic.Azure.Storage.Communications.Core;
using Basic.Azure.Storage.Communications.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Azure.Storage.Communications.BlobService.ContainerOperations
{
    public class GetContainerMetadataResponse : IResponsePayload, IReceiveAdditionalHeadersWithResponse
    {
        public DateTime Date { get; protected set; }

        public string ETag { get; protected set; }

        public DateTime LastModified { get; protected set; }



        public ReadOnlyDictionary<string, string> Metadata { get; protected set; }

        public void ParseHeaders(System.Net.HttpWebResponse response)
        {
            //TODO: determine what we want to do about potential missing headers and date parsing errors

            ETag = response.Headers[ProtocolConstants.Headers.ETag].Trim(new char[] { '"' });
            Date = ParseDate(response.Headers[ProtocolConstants.Headers.OperationDate]);
            LastModified = ParseDate(response.Headers[ProtocolConstants.Headers.LastModified]);

            var metadata = new Dictionary<string, string>();
            foreach (var headerKey in response.Headers.AllKeys)
            {
                if (headerKey.StartsWith(ProtocolConstants.Headers.MetaDataPrefix, StringComparison.InvariantCultureIgnoreCase))
                {
                    metadata[headerKey.Substring(ProtocolConstants.Headers.MetaDataPrefix.Length)] = response.Headers[headerKey];
                }
            }
            Metadata = new ReadOnlyDictionary<string, string>(metadata);
        }

        private DateTime ParseDate(string headerValue)
        {
            DateTime dateValue;
            DateTime.TryParse(headerValue, out dateValue);
            return dateValue;
        }

    }
}
