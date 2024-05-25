using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Rfproduct
    {
        public int PrdId { get; set; }
        public string PrdCode { get; set; }
        public string PrdDesc { get; set; }
        public string PrdCoreCode { get; set; }
        public int? PrdSgmId { get; set; }
        public DateTime? PrdCreateDate { get; set; }
        public DateTime? PrdUpdateDate { get; set; }
        public int? PrdStatusId { get; set; }
    }
}
