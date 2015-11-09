using System;

namespace Basic.Azure.Storage.Communications.Common
{
    [Flags]
    public enum BlobSharedAccessPermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4,
        List = 8,
        Create = 16
    }
}
