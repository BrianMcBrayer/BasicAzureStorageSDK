using System;
using Basic.Azure.Storage.Communications.Core.Interfaces;

namespace Basic.Azure.Storage.Communications.Common
{
    public class BaseSharedAccessSignature : ISharedAccessSignature
    {
        public const RestProtocolVersions ImplementedRestProtocolVersion = RestProtocolVersions._2012_02_12;

        #region ISharedAccessSignature

        public string SignedVersion { get; protected set; }
        public DateTime? SignedStart { get; protected set; }
        public DateTime? SignedExpiry { get; protected set; }
        public int SignedPermissions { get; protected set; }
        public string SignedIdentifier { get; protected set; }

        #endregion

        public BaseSharedAccessSignature(RestProtocolVersions signedVersion, DateTime? signedStart, DateTime? signedExpiry, string signedIdentifier)
        {
            if (signedVersion != ImplementedRestProtocolVersion)
            {
                throw new ArgumentException(string.Format("The given rest protocol version [{0}] is not yet implemented for blob service shared access signatures. Currently supported versions are {1}", signedVersion, ImplementedRestProtocolVersion), "signedVersion");
            }

            // rules for signedExpiry and signedIdentifier...
            // signedExpiry -- required unless signedIdentifier is provided
            // signedIdentifier -- optional
            if (signedExpiry.HasValue && !string.IsNullOrWhiteSpace(signedIdentifier))
            {
                throw new ArgumentException("Shared access signature cannot have both an expiration time and a signed identifier", "signedExpiry");
            }
            else if (!signedExpiry.HasValue && string.IsNullOrWhiteSpace(signedIdentifier))
            {
                throw new ArgumentException("Shared access signature must have either an expiration time or a signed identifier given", "signedExpiry");
            }

            SignedVersion = RestProtocolVersionsMapping.Map[signedVersion];
            SignedStart = signedStart;
            SignedExpiry = signedExpiry;
            SignedIdentifier = signedIdentifier;
        }
    }
}