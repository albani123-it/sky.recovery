using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class LogActivity
    {
        public long LogId { get; set; }
        public string LogCode { get; set; }
        public string LogUserid { get; set; }
        public DateTime? LogActionDate { get; set; }
        public string LogHttps { get; set; }
        public string LogServerName { get; set; }
        public string LogActivity1 { get; set; }
        public string LogPageUrl { get; set; }
        public string LogQueryString { get; set; }
        public string LogBrowser { get; set; }
        public string LogOperatingSystem { get; set; }
        public string ClientIp { get; set; }
    }
}
