using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Permasalahanrestrukture
    {
        public long Id { get; set; }
        public long? Restruktureid { get; set; }
        public string Descriptions { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
    }
}
