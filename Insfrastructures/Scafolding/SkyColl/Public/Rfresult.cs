using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Rfresult
    {
        public int RfrId { get; set; }
        public string RfrRlCode { get; set; }
        public string RfrRlDesc { get; set; }
        public DateTime? RfrCreateDate { get; set; }
        public DateTime? RfrUpdateDate { get; set; }
        public int? RfrStatusId { get; set; }
        public int? RfrIsDc { get; set; }
        public int? RfrIsFc { get; set; }
    }
}
