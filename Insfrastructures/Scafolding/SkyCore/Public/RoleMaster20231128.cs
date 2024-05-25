using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class RoleMaster20231128
    {
        public int? RlmId { get; set; }
        public string RlmName { get; set; }
        public string RlmCode { get; set; }
        public int? RlmParentid { get; set; }
        public int? RlmOrder { get; set; }
        public string RlmLicenseModule { get; set; }
        public bool? RlmIsAction { get; set; }
    }
}
