using System.Collections.Generic;

namespace Basic.Azure.Storage.Communications.Common
{
    public static class RestProtocolVersionsMapping
    {
        public const string _2011_08_18 = "2011-08-18";
        public const string _2012_02_12 = "2012-02-12";
        public const string _2013_08_15 = "2013-08-15";
        public const string _2014_02_14 = "2014-02-14";
        public const string _2015_02_21 = "2015-02-21";
        public const string _2015_04_05 = "2015-04-15";

        private static readonly Dictionary<RestProtocolVersions, string> _versionMapping = new Dictionary<RestProtocolVersions, string>
        {
            { RestProtocolVersions._2011_08_18, _2011_08_18 },
            { RestProtocolVersions._2012_02_12, _2012_02_12 },
            { RestProtocolVersions._2013_08_15, _2013_08_15 },
            { RestProtocolVersions._2014_02_14, _2014_02_14 },
            { RestProtocolVersions._2015_02_21, _2015_02_21 },
            { RestProtocolVersions._2015_04_05, _2015_04_05 }
        };

        public static Dictionary<RestProtocolVersions, string> Map { get { return _versionMapping; } }
    }
}