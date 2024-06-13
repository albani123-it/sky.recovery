using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class RptProcessLog
    {
        public long PolId { get; set; }
        public long PolAppId { get; set; }
        public string PolTtable { get; set; }
        public int? PolWfId { get; set; }
        public string PolUsr { get; set; }
        public string PolSourceId { get; set; }
        public DateTime? PolActionDate { get; set; }
        public string PolStatus { get; set; }
        public DateTime? PolFinishDate { get; set; }
        public string PolData { get; set; }
        public string PolEdgesId { get; set; }
        public string PolNotes { get; set; }
    }
}
