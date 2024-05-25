using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class AccountDistribution
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Dpd { get; set; }
        public int? DpdMin { get; set; }
        public int? DpdMax { get; set; }
        public int? MaxPtp { get; set; }
        public string CoreCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? StatusId { get; set; }
    }
}
