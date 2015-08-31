﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basic.Azure.Storage.Communications.BlobService.BlobOperations;
using Basic.Azure.Storage.Tests.Fakes;
using NUnit.Framework;
using TestableHttpWebResponse;
using TestableHttpWebResponse.ResponseSettings;

namespace Basic.Azure.Storage.Tests.Communications.BlobService.BlobOperations
{
    [TestFixture]
    public class PutBlockRequestTests
    {
        private StorageAccountSettings _settings;

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            _settings = new SettingsFake();
            WebRequest.RegisterPrefix("test", TestableWebRequestCreateFactory.GetFactory());
        }

        [Test]
        public void Execute_PutBlock_ResponseParsesHeadersCorrectly()
        {
            var expectedContainer = "test-container";
            var expectedBlob = "test-blob";
            var expectedBlock = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("test-block-id"));
            var urlEncodedExpectedBlock = WebUtility.UrlEncode(expectedBlock);
            var expectedDate = DateTime.UtcNow;
            var expectedMD5 = "test-MD5";
            var expectedData = new byte[] {};

            var expectedUri = String.Format("{0}/{1}/{2}?comp=block&blockid={3}", _settings.BlobEndpoint, expectedContainer, expectedBlob, urlEncodedExpectedBlock);
            var expectedRawRequest = new TestableWebRequest(new Uri(expectedUri))
                                            .EnqueueResponse(new HttpResponseSettings((HttpStatusCode)201, "Created", "", false, new Dictionary<string, string>(){
                                                {"Content-MD5", expectedMD5},
                                                {"Date", expectedDate.ToString() }
                                            }));
            TestableWebRequestCreateFactory.GetFactory().AddRequest(expectedRawRequest);

            var request = new PutBlockRequest(_settings, expectedContainer, expectedBlob, expectedBlock, expectedData);

            var response = request.Execute();

            Assert.AreEqual(response.HttpStatus, HttpStatusCode.Created);
            Assert.AreEqual(response.Payload.ContentMD5, expectedMD5);
            Assert.IsTrue(Math.Abs(expectedDate.Subtract(response.Payload.Date).TotalMinutes) < 1);
        }

        [Test]
        public async Task Execute_PutBlockAsync_ResponseParsesHeadersCorrectly()
        {
            var expectedContainer = "test-container";
            var expectedBlob = "test-blob";
            var expectedBlock = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("test-block-id"));
            var urlEncodedExpectedBlock = WebUtility.UrlEncode(expectedBlock);
            var expectedDate = DateTime.UtcNow;
            var expectedMD5 = "test-MD5";
            var expectedData = new byte[] { };

            var expectedUri = String.Format("{0}/{1}/{2}?comp=block&blockid={3}", _settings.BlobEndpoint, expectedContainer, expectedBlob, urlEncodedExpectedBlock);
            var expectedRawRequest = new TestableWebRequest(new Uri(expectedUri))
                                            .EnqueueResponse(new HttpResponseSettings((HttpStatusCode)201, "Created", "", false, new Dictionary<string, string>(){
                                                {"Content-MD5", expectedMD5},
                                                {"Date", expectedDate.ToString() }
                                            }));
            TestableWebRequestCreateFactory.GetFactory().AddRequest(expectedRawRequest);

            var request = new PutBlockRequest(_settings, expectedContainer, expectedBlob, expectedBlock, expectedData);

            var response = await request.ExecuteAsync();

            Assert.AreEqual(response.HttpStatus, HttpStatusCode.Created);
            Assert.AreEqual(response.Payload.ContentMD5, expectedMD5);
            Assert.IsTrue(Math.Abs(expectedDate.Subtract(response.Payload.Date).TotalMinutes) < 1);
        }
    }
}
