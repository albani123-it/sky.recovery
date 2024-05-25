using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class AreaReq
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public int? BranchId { get; set; }
        public string Value { get; set; }
        public string Adesc { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? StatusId { get; set; }
    }
}
