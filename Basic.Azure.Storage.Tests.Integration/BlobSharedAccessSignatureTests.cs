using System;
using System.Linq;
using System.Net;
using System.Web;
using Basic.Azure.Storage.Communications.BlobService;
using Basic.Azure.Storage.Communications.Common;
using Microsoft.WindowsAzure.Storage.Blob;
using NUnit.Framework;

namespace Basic.Azure.Storage.Tests.Integration
{
    [TestFixture]
    public class BlobSharedAccessSignatureTests : BaseBlobServiceClientTestFixture
    {

        [Test]
        public void GetBlob_UsingSharedAccessSignatureWithRequiredFieldsOnly_GetsBlob()
        {
            const string expectedContent = "Expected blob content";
            var containerName = GenerateSampleContainerName();
            var blobName = GenerateSampleBlobName();
            CreateContainer(containerName);
            var blob = CreateBlockBlob(containerName, blobName, content: expectedContent);
            var startTime = new DateTime(2015, 11, 10, 12, 12, 12);
            var expiryTime = startTime.AddHours(1);
            var expectedAccessSignature = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = startTime,
                SharedAccessExpiryTime = expiryTime,
                Permissions = SharedAccessBlobPermissions.Read
            };
            var expectedAccessSignatureString = blob.GetSharedAccessSignature(expectedAccessSignature, null, null, "2012-02-12");
            var sharedAccessSignature = new BlobSharedAccessSignature(RestProtocolVersions._2012_02_12, startTime, expiryTime, BlobSharedAccessPermissions.Read, null, containerName, blobName);
            var generatedSignature = sharedAccessSignature.GenerateSharedAccessSignatureString(AccountSettings);
            var accessUri = new UriBuilder(blob.Uri.AbsoluteUri);
            accessUri.Query = accessUri.Query + generatedSignature;
            //var expectedRequest = WebRequest.Create(blob.Uri.AbsoluteUri + expectedAccessSignatureString);
            //var request = WebRequest.Create(accessUri.Uri);

            //var expectedResponse = (HttpWebResponse)expectedRequest.GetResponse();
            //var response = (HttpWebResponse)request.GetResponse();

            var parsedExpected = expectedAccessSignatureString.Replace("?", "").Split('&')
                .ToList();
            Console.WriteLine("decoded their sig is {0}",HttpUtility.UrlDecode(parsedExpected.First(f => f.StartsWith("sig"))));
            Console.WriteLine(parsedExpected.First(f => f.StartsWith("sig")));
            parsedExpected.Sort();
            var compareExpected = string.Join("&", parsedExpected);

            var parsedGiven = generatedSignature.Split('&')
                .ToList();
            Console.WriteLine(parsedGiven.First(f => f.StartsWith("sig")));
            parsedGiven.Sort();
            var compareGiven = string.Join("&", parsedGiven);

            //Console.WriteLine(compareExpected);
            //Console.WriteLine(compareGiven);

            Assert.AreEqual(compareExpected, compareGiven);
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}