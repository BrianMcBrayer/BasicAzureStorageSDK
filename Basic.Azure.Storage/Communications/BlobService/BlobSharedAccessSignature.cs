using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Basic.Azure.Storage.Communications.Common;
using Basic.Azure.Storage.Communications.Core.Interfaces;

namespace Basic.Azure.Storage.Communications.BlobService
{
    public class BlobSharedAccessSignature : BaseSharedAccessSignature, ISharedAccessSignature
    {
        #region ISharedAccessSignature

        new int SignedPermissions { get { return (int)BlobSignedPermissions; } }

        public override string GenerateStringToSign(StorageAccountSettings settings)
        {
            var stringToSign =
                string.Join("\n",
                GenerateBlobSignedPermissions(BlobSignedPermissions),
                ConvertSignedNullableDate(SignedStart),
                ConvertSignedNullableDate(SignedExpiry),
                GenerateCanonicalizedResourceString(settings, ContainerName, BlobName),
                SignedIdentifier,
                SignedVersion);

            return stringToSign;
        }

        #endregion

        public string SignedResourceType { get { return "b" /* b - blob ; c - container */; } }

        public BlobSharedAccessPermissions BlobSignedPermissions { get; set; }

        public string ContainerName { get; set; }

        public string BlobName { get; set; }

        public BlobSharedAccessSignature(RestProtocolVersions signedVersion, DateTime? signedStart, DateTime? signedExpiry, BlobSharedAccessPermissions signedPermissions, string signedIdentifier, string containerName, string blobName)
            : base(signedVersion, signedStart, signedExpiry, signedIdentifier)
        {
            BlobSignedPermissions = signedPermissions;
            ContainerName = containerName;
            BlobName = blobName;
        }

        private static string GenerateBlobSignedPermissions(BlobSharedAccessPermissions permissions)
        {
            var permissionString = new StringBuilder();

            if (permissions.HasFlag(BlobSharedAccessPermissions.Read))
                permissionString.Append("r");

            if (permissions.HasFlag(BlobSharedAccessPermissions.Write))
                permissionString.Append("w");

            if (permissions.HasFlag(BlobSharedAccessPermissions.Delete))
                permissionString.Append("d");

            return permissionString.ToString();
        }

        private static string GenerateCanonicalizedResourceString(StorageAccountSettings settings, string containerName, string blobName)
        {
            // format is accountName/containerName/blobName
            // remember that .NET is weird with URIs between 4.5 and 4.6 -- specifically it uses \ instead of / sometimes
            const string canonicalFormat = "{0}/{1}/{2}";
            var fixedBlobName = blobName.Replace('\\', '/');
            return string.Format(CultureInfo.InvariantCulture, canonicalFormat, settings.AccountName, containerName, fixedBlobName);
        }

        protected override Dictionary<string, string> GenerateSharedAccessSignatureQueryParts(StorageAccountSettings settings)
        {
            return new Dictionary<string, string>
            {
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.SignedVersion], SignedVersion},
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.SignedResource], SignedResourceType},
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.SignedStart], ConvertSignedNullableDate(SignedStart)},
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.SignedExpiry], ConvertSignedNullableDate(SignedExpiry)},
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.SignedPermissions], GenerateBlobSignedPermissions(BlobSignedPermissions)},
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.Signature], GenerateSignature(settings)},
                {SharedAccessSignatureFieldNameParameterMapping.Map[SharedAccessSignatureFieldName.SignedIdentifier], SignedIdentifier},
            };
        }

    }
}