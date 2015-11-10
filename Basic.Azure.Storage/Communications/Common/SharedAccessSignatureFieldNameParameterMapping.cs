using System.Collections.Generic;

namespace Basic.Azure.Storage.Communications.Common
{
    public static class SharedAccessSignatureFieldNameParameterMapping
    {
        public const string SignedVersionQuery = "sv";
        public const string SignedResourceQuery = "sr";
        public const string SignedStartQuery = "st";
        public const string SignedExpiryQuery = "se";
        public const string SignedPermissionsQuery = "sp";
        public const string SignatureQuery = "sig";
        public const string SignedIdentifierQuery = "si";

        private static readonly Dictionary<SharedAccessSignatureFieldName, string> _mapping = new Dictionary<SharedAccessSignatureFieldName, string>
        {
            { SharedAccessSignatureFieldName.SignedVersion, SignedVersionQuery },
            { SharedAccessSignatureFieldName.SignedResource, SignedResourceQuery },
            { SharedAccessSignatureFieldName.SignedStart, SignedStartQuery },
            { SharedAccessSignatureFieldName.SignedExpiry, SignedExpiryQuery },
            { SharedAccessSignatureFieldName.SignedPermissions, SignedPermissionsQuery },
            { SharedAccessSignatureFieldName.Signature, SignatureQuery },
            { SharedAccessSignatureFieldName.SignedIdentifier, SignedIdentifierQuery }
        };

        public static Dictionary<SharedAccessSignatureFieldName, string> Map { get { return _mapping; } }
    }
}