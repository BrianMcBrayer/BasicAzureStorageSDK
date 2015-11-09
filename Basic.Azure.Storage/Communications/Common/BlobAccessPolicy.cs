using System;

namespace Basic.Azure.Storage.Communications.Common
{
    public class BlobAccessPolicy
    {
        public DateTime StartTime { get; set; }
        public DateTime Expiry { get; set; }
        public BlobSharedAccessPermissions Permission { get; set; }
    }
}
