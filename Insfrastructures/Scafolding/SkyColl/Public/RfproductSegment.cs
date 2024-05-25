using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class RfproductSegment
    {
        public int PrdSgmId { get; set; }
        public string PrdSgmCode { get; set; }
        public string PrdSgmDesc { get; set; }
        public string PrdSgmCoreCode { get; set; }
        public DateTime? PrdSgmCreateDate { get; set; }
        public DateTime? PrdSgmUpdateDate { get; set; }
        public int? PrdSgmStatusId { get; set; }
    }
}
