using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Basic.Azure.Storage.Communications.Core.Interfaces;

namespace Basic.Azure.Storage.Communications.Common
{
    public abstract class BaseSharedAccessSignature : ISharedAccessSignature
    {
        public const RestProtocolVersions ImplementedRestProtocolVersion = RestProtocolVersions._2012_02_12;

        #region ISharedAccessSignature

        public string SignedVersion { get; protected set; }
        public DateTime? SignedStart { get; protected set; }
        public DateTime? SignedExpiry { get; protected set; }
        public int SignedPermissions { get; protected set; }
        public string SignedIdentifier { get; protected set; }

        public string GenerateSignature(StorageAccountSettings settings)
        {
            var signature = settings.ComputeMacSha256(GenerateStringToSign(settings));

            Console.WriteLine("sig is {0}", signature);
            Console.WriteLine("encoded sig is {0}", HttpUtility.UrlEncode(signature));

            return signature;
        }

        public abstract string GenerateStringToSign(StorageAccountSettings settings);

        public string GenerateSharedAccessSignatureString(StorageAccountSettings settings)
        {
            const string urlQueryPairFormat = "{0}={1}";

            var parts = string.Join("&", GenerateSharedAccessSignatureQueryParts(settings)
                .Where(pair => !string.IsNullOrWhiteSpace(pair.Value))
                .Select(pair => string.Format(urlQueryPairFormat, pair.Key, HttpUtility.UrlEncode(pair.Value))));

            return parts;
        }

        #endregion

        protected BaseSharedAccessSignature(RestProtocolVersions signedVersion, DateTime? signedStart, DateTime? signedExpiry, string signedIdentifier)
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

        protected string ConvertSignedNullableDate(DateTime? signedDate)
        {
            return signedDate.HasValue
                ? signedDate.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture)
                : null;
        }

        protected abstract Dictionary<string, string> GenerateSharedAccessSignatureQueryParts(StorageAccountSettings settings);
    }
}