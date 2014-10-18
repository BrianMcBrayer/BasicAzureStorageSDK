﻿using Basic.Azure.Storage.ClientContracts;
using Basic.Azure.Storage.Communications.BlobService;
using Basic.Azure.Storage.Communications.BlobService.BlobOperations;
using Basic.Azure.Storage.Communications.BlobService.ContainerOperations;
using Basic.Azure.Storage.Communications.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Azure.Storage
{
    public class BlobServiceClient : IBlobStorageClient
    {
        private StorageAccountSettings _account;

        public BlobServiceClient(StorageAccountSettings account)
        {
            _account = account;
        }

        #region Account Operations


        #endregion

        #region Container Operations

        public CreateContainerResponse CreateContainer(string containerName, ContainerAccessType containerAccessType)
        {
            var request = new CreateContainerRequest(_account, containerName, containerAccessType);
            var response = request.Execute();
            return response.Payload;
        }
        public async Task<CreateContainerResponse> CreateContainerAsync(string containerName, ContainerAccessType containerAccessType)
        {
            var request = new CreateContainerRequest(_account, containerName, containerAccessType);
            var response = await request.ExecuteAsync();
            return response.Payload;
        }

        public GetContainerPropertiesResponse GetContainerProperties(string containerName)
        {
            var request = new GetContainerPropertiesRequest(_account, containerName);
            var response = request.Execute();
            return response.Payload;
        }
        public async Task<GetContainerPropertiesResponse> GetContainerPropertiesAsync(string containerName)
        {
            var request = new GetContainerPropertiesRequest(_account, containerName);
            var response = await request.ExecuteAsync();
            return response.Payload;
        }

        public GetContainerMetadataResponse GetContainerMetadata(string containerName)
        {
            var request = new GetContainerMetadataRequest(_account, containerName);
            var response = request.Execute();
            return response.Payload;
        }
        public async Task<GetContainerMetadataResponse> GetContainerMetadataAsync(string containerName)
        {
            var request = new GetContainerMetadataRequest(_account, containerName);
            var response = await request.ExecuteAsync();
            return response.Payload;
        }

        public void SetContainerMetadata(string containerName, Dictionary<string, string> metadata, string lease = null)
        {
            var request = new SetContainerMetadataRequest(_account, containerName, metadata, lease);
            request.Execute();
        }
        public async Task SetContainerMetadataAsync(string containerName, Dictionary<string, string> metadata, string lease = null)
        {
            var request = new SetContainerMetadataRequest(_account, containerName, metadata, lease);
            await request.ExecuteAsync();
        }

        public GetContainerACLResponse GetContainerACL(string containerName)
        {
            var request = new GetContainerACLRequest(_account, containerName);
            var response = request.Execute();
            return response.Payload;
        }
        public async Task<GetContainerACLResponse> GetContainerACLAsync(string containerName)
        {
            var request = new GetContainerACLRequest(_account, containerName);
            var response = await request.ExecuteAsync();
            return response.Payload;
        }

        public void SetContainerACL(string containerName, ContainerAccessType containerAccess, List<BlobSignedIdentifier> signedIdentifiers, string leaseId = null)
        {
            var request = new SetContainerACLRequest(_account, containerName, containerAccess, signedIdentifiers, leaseId);
            request.Execute();
        }
        public async Task SetContainerACLAsync(string containerName, ContainerAccessType containerAccess, List<BlobSignedIdentifier> signedIdentifiers, string leaseId = null)
        {
            var request = new SetContainerACLRequest(_account, containerName, containerAccess, signedIdentifiers, leaseId);
            await request.ExecuteAsync();
        }

        public void DeleteContainer(string containerName, string leaseId = null)
        {
            var request = new DeleteContainerRequest(_account, containerName, leaseId);
            request.Execute();
        }
        public async Task DeleteContainerAsync(string containerName, string leaseId = null)
        {
            var request = new DeleteContainerRequest(_account, containerName, leaseId);
            await request.ExecuteAsync();
        }


        #endregion

        #region Blob Operations

        /// <summary>
        /// Creates a new BlockBlob (Alias for the PutBlob call with a Blob Type of BlockBlob)
        /// </summary>
        public PutBlobResponse PutBlockBlob(string containerName, string blobName, byte[] data,
            string contentType = null, string contentEncoding = null, string contentLanguage = null, string contentMD5 = null,
            string cacheControl = null, Dictionary<string, string> metadata = null)
        {
            var request = new PutBlobRequest(_account, containerName, blobName, data, contentType, contentEncoding, contentLanguage, contentMD5, cacheControl, metadata);
            var response = request.Execute();
            return response.Payload;
        }
        public async Task<PutBlobResponse> PutBlockBlobAsync(string containerName, string blobName, byte[] data,
            string contentType = null, string contentEncoding = null, string contentLanguage = null, string contentMD5 = null,
            string cacheControl = null, Dictionary<string, string> metadata = null)
        {
            var request = new PutBlobRequest(_account, containerName, blobName, data, contentType, contentEncoding, contentLanguage, contentMD5, cacheControl, metadata);
            var response = await request.ExecuteAsync();
            return response.Payload;
        }

        /// <summary>
        /// Creates a new PageBlob (Alias for the PutBlob call with a Blob Type of PageBlob)
        /// </summary>
        public PutBlobResponse PutPageBlob(string containerName, string blobName, int contentLength,
            string contentType = null, string contentEncoding = null, string contentLanguage = null, string contentMD5 = null,
            string cacheControl = null, Dictionary<string, string> metadata = null, long sequenceNumber = 0)
        {
            var request = new PutBlobRequest(_account, containerName, blobName, contentLength, contentType, contentEncoding, contentLanguage, contentMD5, cacheControl, metadata, sequenceNumber);
            var response = request.Execute();
            return response.Payload;
        }
        public async Task<PutBlobResponse> PutPageBlobAsync(string containerName, string blobName, int contentLength,
            string contentType = null, string contentEncoding = null, string contentLanguage = null, string contentMD5 = null,
            string cacheControl = null, Dictionary<string, string> metadata = null, long sequenceNumber = 0)
        {
            var request = new PutBlobRequest(_account, containerName, blobName, contentLength, contentType, contentEncoding, contentLanguage, contentMD5, cacheControl, metadata, sequenceNumber);
            var response = await request.ExecuteAsync();
            return response.Payload;
        }
        #endregion


    }
}
