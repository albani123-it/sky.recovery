using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Workflowconfig
    {
        public long Id { get; set; }
        public int? Workflowconfigdetailid { get; set; }
        public string Roles { get; set; }
        public int? Orderstep { get; set; }
        public string Testing { get; set; }
        public string Testing2 { get; set; }
    }
}
