using System;
using Basic.Azure.Storage.Communications.Common;
using Basic.Azure.Storage.Communications.Core.Interfaces;

namespace Basic.Azure.Storage.Communications.BlobService
{
    public class BlobSharedAccessSignature : BaseSharedAccessSignature, ISharedAccessSignature
    {
        #region ISharedAccessSignature

        new int SignedPermissions { get { return (int)BlobSignedPermissions; } }

        #endregion

        public BlobSignedResourceType SignedResourceType { get { return BlobSignedResourceType.Blob; } }

        public BlobSharedAccessPermissions BlobSignedPermissions { get; set; }

        public BlobSharedAccessSignature(RestProtocolVersions signedVersion, DateTime? signedStart, DateTime? signedExpiry, BlobSharedAccessPermissions signedPermissions, string signedIdentifier)
            : base(signedVersion, signedStart, signedExpiry, signedIdentifier)
        {
            BlobSignedPermissions = signedPermissions;
        }

    }
}