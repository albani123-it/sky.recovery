using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class ResponseResult
    {
        public long RreId { get; set; }
        public string RreRshid { get; set; }
        public string RreObjectCode { get; set; }
        public string RreResult { get; set; }
        public DateTime? RreTimestamp { get; set; }
    }
}
