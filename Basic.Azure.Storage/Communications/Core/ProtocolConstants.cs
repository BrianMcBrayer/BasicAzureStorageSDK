﻿using Basic.Azure.Storage.Communications.TableService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Azure.Storage.Communications.Core
{
    public static class ProtocolConstants
    {

        public static class Headers
        {
            public const string ApproximateMessagesCount = "x-ms-approximate-messages-count";

            public const string Authorization = "Authorization";

            public const string BlobContentLength = "x-ms-blob-content-length";

            public const string BlobPublicAccess = "x-ms-blob-public-access";

            public const string BlobSequenceNumber = "x-ms-blob-sequence-number";

            public const string BlobType = "x-ms-blob-type";

            public const string CacheControl = "Cache-Control";

            public const string ContentEncoding = "Content-Encoding";

            public const string ContentLanguage = "Content-Language";

            public const string ContentMD5 = "Content-MD5";

            public const string Date = "x-ms-date";

            public const string ETag = "ETag";

            public const string LastModified = "Last-Modified";

            public const string MetaDataPrefix = "x-ms-meta-";

            public const string OperationDate = "Date";

            public const string PreferenceApplied = "Preference-Applied";

            public const string RequestId = "x-ms-request-id";

            public const string StorageVersion = "x-ms-version";

            public const string UserAgent = "User-Agent";

            public const string Version = "x-ms-version";
        }

        public static class HeaderValues
        {
            
            public static class BlobPublicAccess
            {

                public const string Container = "container";

                public const string Blob = "blob";

            }

            public static class BlobType
            {

                public const string Block = "BlockBlob";

                public const string Page = "PageBlob";

            }

            public static class TableMetadataPreference
            {
                public const string ReturnContent = "return-content";

                public const string ReturnNoContent = "return-no-content";

                public static string GetValue(MetadataPreference value)
                {
                    switch (value)
                    {
                        case MetadataPreference.ReturnContent:
                            return ReturnContent;
                        case MetadataPreference.ReturnNoContent:
                        default:
                            return ReturnNoContent;
                    }
                }
            }
        }

        public static class QueryParameters
        {
            public const string Comp = "comp";

            public const string MessageTTL = "messagettl";

            public const string NumOfMessages = "numofmessages";

            public const string ResType = "restype";

            public const string Timeout = "timeout";

            public const string VisibilityTimeout = "visibilitytimeout";

        }

        public static class QueryValues
        {
            public const string ACL = "acl";

            public const string Metadata = "metadata";

            public static class ResType
            {

                public const string Container = "container";

            }


        }
    }
}
