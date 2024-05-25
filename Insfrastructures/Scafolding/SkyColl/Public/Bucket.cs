using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Bucket
    {
        public int BctId { get; set; }
        public string BctCode { get; set; }
        public string BctName { get; set; }
        public string BctExtCode { get; set; }
        public int? BctStatusId { get; set; }
        public string BctCreatedBy { get; set; }
        public string BctModifiedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
