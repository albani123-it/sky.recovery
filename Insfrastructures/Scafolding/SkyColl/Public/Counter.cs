using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Counter
    {
        public int Id { get; set; }
        public string CounterType { get; set; }
        public int? BranchId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Ctr { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? StatusId { get; set; }
    }
}
