﻿using Basic.Azure.Storage.Communications.Core;
using Basic.Azure.Storage.Communications.Core.Interfaces;
using Basic.Azure.Storage.Communications.Utility;

namespace Basic.Azure.Storage.Communications.BlobService.BlobOperations
{
    /// <summary>
    /// Lease Blob - Only the Break Lease action
    /// https://msdn.microsoft.com/en-us/library/azure/ee691972.aspx
    /// </summary>
    /// <remarks>
    /// This is separated into multiple requests because they have different 
    /// behavior and responses depending on the Lease Action. I think very
    /// discrete requests and responses will be easier to work with, despite
    /// the slight change from the common pattern of 1 request per API call.
    /// </remarks>
    public class LeaseBlobBreakRequest : RequestBase<EmptyResponsePayload>, ISendAdditionalOptionalHeaders
    {
        private readonly string _containerName;
        private readonly string _blobName;
        private readonly string _leaseId;
        private readonly int _leaseBreakPeriod;

        public LeaseBlobBreakRequest(StorageAccountSettings settings, string containerName, string blobName, string leaseId, int leaseBreakPeriod)
            : base(settings)
        {
            Guard.ArgumentIsNotNullOrEmpty("containerName", containerName);
            Guard.ArgumentIsNotNullOrEmpty("blobName", blobName);
            Guard.ArgumentIsAGuid("leaseId", leaseId);
            Guard.ArgumentInRanges("leaseBreakPeriod", leaseBreakPeriod, new GuardRange<int>(0, 60));

            _containerName = containerName;
            _blobName = blobName;
            _leaseId = leaseId;
            _leaseBreakPeriod = leaseBreakPeriod;
        }

        protected override string HttpMethod { get { return "PUT"; } }

        protected override StorageServiceType ServiceType { get { return StorageServiceType.BlobService; } }

        protected override RequestUriBuilder GetUriBase()
        {
            var builder = new RequestUriBuilder(Settings.BlobEndpoint);
            builder.AddSegment(_containerName);
            builder.AddSegment(_blobName);
            builder.AddParameter(ProtocolConstants.QueryParameters.Comp, ProtocolConstants.QueryValues.Comp.Lease);
            return builder;
        }

        public void ApplyAdditionalOptionalHeaders(System.Net.WebRequest request)
        {
            request.Headers.Add(ProtocolConstants.Headers.LeaseAction, ProtocolConstants.HeaderValues.LeaseAction.Break);
            request.Headers.Add(ProtocolConstants.Headers.LeaseId, _leaseId);
            request.Headers.Add(ProtocolConstants.Headers.LeaseBreakPeriod, _leaseBreakPeriod.ToString());
        }
    }
}
