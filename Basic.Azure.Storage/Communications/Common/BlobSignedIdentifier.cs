namespace Basic.Azure.Storage.Communications.Common
{
    public class BlobSignedIdentifier
    {
        public string Id { get; set; }

        public BlobAccessPolicy AccessPolicy { get; set; }
    }
}
