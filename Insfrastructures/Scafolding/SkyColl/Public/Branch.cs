using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Branch
    {
        public long LbrcId { get; set; }
        public string LbrcCode { get; set; }
        public string LbrcName { get; set; }
        public string LbrcAddress { get; set; }
        public string LbrcCity { get; set; }
        public string LbrcPhoneNum { get; set; }
        public bool? LbrcIsDelete { get; set; }
        public string LbrcGroup { get; set; }
    }
}
