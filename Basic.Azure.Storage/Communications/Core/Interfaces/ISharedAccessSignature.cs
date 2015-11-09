using System;

namespace Basic.Azure.Storage.Communications.Core.Interfaces
{
    public interface ISharedAccessSignature
    {
        string SignedVersion { get; }

        DateTime? SignedStart { get; }

        DateTime? SignedExpiry { get; }

        int SignedPermissions { get; }

        string SignedIdentifier { get; }
    }
}