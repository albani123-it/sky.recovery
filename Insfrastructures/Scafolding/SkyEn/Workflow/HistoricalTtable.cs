using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class HistoricalTtable
    {
        public long HttId { get; set; }
        public string HttRshid { get; set; }
        public string HttTtable { get; set; }
        public string HttSourceId { get; set; }
        public string HttWfId { get; set; }
        public string HttData { get; set; }
        public string HttUserid { get; set; }
        public DateTime? HttTimestamp { get; set; }
    }
}
