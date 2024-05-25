using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class RoleLog
    {
        public long Id { get; set; }
        public int? RlId { get; set; }
        public string RlName { get; set; }
        public string RlDescription { get; set; }
        public bool? RlStatus { get; set; }
        public string RlActionBy { get; set; }
        public DateTime? RlLogDate { get; set; }
        public string RlLogAction { get; set; }
        public long? RlLogid { get; set; }
    }
}
