using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class ReverseAppApproval
    {
        public long RaaId { get; set; }
        public string RaaAppid { get; set; }
        public string RaaAplnumber { get; set; }
        public string RaaLogid { get; set; }
        public string RaaLastSource { get; set; }
        public string RaaRevObject { get; set; }
        public string RaaRevEdgeid { get; set; }
        public string RaaRevObjectType { get; set; }
        public string RaaNotes { get; set; }
        public string RaaStatus { get; set; }
        public string RaaRequestedBy { get; set; }
        public string RaaApprovedBy { get; set; }
        public DateTime? RaaRequest { get; set; }
        public DateTime? RaaApproval { get; set; }
    }
}
