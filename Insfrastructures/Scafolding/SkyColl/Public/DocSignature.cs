using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class DocSignature
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string DocCode { get; set; }
        public string DocName { get; set; }
    }
}
