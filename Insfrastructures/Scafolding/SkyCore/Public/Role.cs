using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class Role
    {
        public int RlId { get; set; }
        public string RlName { get; set; }
        public string RlDescription { get; set; }
        public bool? RlStatus { get; set; }
        public string RlCreatedBy { get; set; }
        public DateTime? RlCreatedDate { get; set; }
        public string RlUpdatedBy { get; set; }
        public DateTime? RlUpdatedDate { get; set; }
    }
}
