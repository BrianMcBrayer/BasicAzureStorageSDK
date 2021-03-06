﻿using Basic.Azure.Storage.Communications.Common;
using Basic.Azure.Storage.Communications.Core;
using Basic.Azure.Storage.Communications.Core.Interfaces;
using Basic.Azure.Storage.Communications.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Basic.Azure.Storage.Communications.QueueService.AccountOperations
{
    public class GetQueueServicePropertiesResponse : IResponsePayload, IReceiveAdditionalHeadersWithResponse, IReceiveDataWithResponse
    {
        public virtual StorageServiceProperties Properties { get; private set; }

        public virtual string RequestId { get; protected set; }

        public virtual string Version { get; protected set; }


        public void ParseHeaders(System.Net.HttpWebResponse response)
        {
            RequestId = response.Headers[ProtocolConstants.Headers.RequestId];
            Version = response.Headers[ProtocolConstants.Headers.Version];
        }

        public async Task ParseResponseBodyAsync(Stream responseStream, string contentType)
        {
            Properties = new StorageServiceProperties();

            using (StreamReader sr = new StreamReader(responseStream))
            {
                var content = await sr.ReadToEndAsync();
                if (content.Length > 0)
                {
                    var xDoc = XDocument.Parse(content);
                    foreach (var topField in xDoc.Root.Elements())
                    {
                        if (topField.Name.LocalName.Equals("Logging", StringComparison.InvariantCultureIgnoreCase))
                        {
                            foreach (var field in topField.Elements())
                            {
                                switch (field.Name.LocalName)
                                {
                                    case "Version":
                                        Properties.Logging.Version = StorageAnalyticsVersionNumber.v1_0;
                                        break;
                                    case "Delete":
                                        Properties.Logging.Delete = field.Value.Equals("true");
                                        break;
                                    case "Read":
                                        Properties.Logging.Read = field.Value.Equals("true");
                                        break;
                                    case "Write":
                                        Properties.Logging.Write = field.Value.Equals("true");
                                        break;
                                    case "RetentionPolicy":
                                        foreach (var retentionField in field.Elements())
                                        {
                                            switch (retentionField.Name.LocalName)
                                            {
                                                case "Enabled":
                                                    Properties.Logging.RetentionPolicyEnabled = retentionField.Value.Equals("true");
                                                    break;
                                                case "Days":
                                                    Properties.Logging.RetentionPolicyNumberOfDays = int.Parse(retentionField.Value);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        else if (topField.Name.LocalName.Equals("HourMetrics", StringComparison.InvariantCultureIgnoreCase))
                        {
                            PopulateMetrics(Properties.HourMetrics, topField);
                        }
                        else if (topField.Name.LocalName.Equals("MinuteMetrics", StringComparison.InvariantCultureIgnoreCase))
                        {
                            PopulateMetrics(Properties.MinuteMetrics, topField);
                        }
                        else if (topField.Name.LocalName.Equals("Cors", StringComparison.InvariantCultureIgnoreCase))
                        {
                            foreach(var corsRuleFields in topField.Elements())
                            {
                                var corsRule = new StorageServiceCorsRule();
                                foreach (var field in corsRuleFields.Elements())
                                {
                                    switch (field.Name.LocalName)
                                    {
                                        case "AllowedOrigins":
                                            corsRule.AllowedOrigins = field.Value.Split(',').ToList();
                                            break;
                                        case "AllowedMethods":
                                            corsRule.AllowedMethods = field.Value.Split(',').ToList();
                                            break;
                                        case "MaxAgeInSeconds":
                                            corsRule.MaxAgeInSeconds = int.Parse(field.Value);
                                            break;
                                        case "ExposedHeaders":
                                            corsRule.ExposedHeaders = field.Value.Split(',').ToList();
                                            break;
                                        case "AllowedHeaders":
                                            corsRule.AllowedHeaders = field.Value.Split(',').ToList();
                                            break;
                                    }
                                }
                                Properties.Cors.Add(corsRule);
                            }
                        }
                    }

                    
                }
            }
        }

        private void PopulateMetrics(StorageServiceMetricsProperties metricsProperty, XElement topField)
        {
            foreach (var field in topField.Elements())
            {
                switch (field.Name.LocalName)
                {
                    case "Version":
                        metricsProperty.Version = StorageAnalyticsVersionNumber.v1_0;
                        break;
                    case "Enabled":
                        metricsProperty.Enabled = field.Value.Equals("true");
                        break;
                    case "IncludeAPIs":
                        metricsProperty.IncludeAPIs = field.Value.Equals("true");
                        break;
                    case "RetentionPolicy":
                        foreach (var retentionField in field.Elements())
                        {
                            switch (retentionField.Name.LocalName)
                            {
                                case "Enabled":
                                    metricsProperty.RetentionPolicyEnabled = retentionField.Value.Equals("true");
                                    break;
                                case "Days":
                                    metricsProperty.RetentionPolicyNumberOfDays = int.Parse(retentionField.Value);
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
