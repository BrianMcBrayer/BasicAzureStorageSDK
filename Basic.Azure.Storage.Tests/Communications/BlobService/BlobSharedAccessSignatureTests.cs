using System;
using Basic.Azure.Storage.Communications.BlobService;
using Basic.Azure.Storage.Communications.Common;
using NUnit.Framework;

namespace Basic.Azure.Storage.Tests.Communications.BlobService
{

    [TestFixture]
    public class BlobSharedAccessSignatureTests
    {

        //[Test]
        //public void GenerateSharedAccessSignatureString_RequiredParams_EqualsMSAzureSDKSASString()
        //{
        //    var devAccount = new LocalEmulatorAccountSettings();
        //    var msDevAccount = new 
        //    var sharedAccessSignature = new BlobSharedAccessSignature(RestProtocolVersions._2012_02_12, DateTime.Now, DateTime.Now.AddDays(1), BlobSharedAccessPermissions.Read | BlobSharedAccessPermissions.Write, "", "testContainer", "testBlob");

        //    var generatedSignatureString = sharedAccessSignature.GenerateSharedAccessSignatureString(devAccount);

        //    Assert.NotNull(generatedSignatureString);
        //}

    }
}