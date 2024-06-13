using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class PrivilegeForm
    {
        public int PrfId { get; set; }
        public int? PrfPriId { get; set; }
        public string PrfFormCode { get; set; }
        public string PrfFormType { get; set; }
        public string PrfFormAccess { get; set; }
    }
}
