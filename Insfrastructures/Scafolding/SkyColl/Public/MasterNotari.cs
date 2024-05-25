using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class MasterNotari
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string NotarisName { get; set; }
        public string NotarisAddress { get; set; }
        public int? StatusId { get; set; }
    }
}
