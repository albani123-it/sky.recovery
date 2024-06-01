using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Masterworkflowrule
    {
        public long Id { get; set; }
        public int? Masterworkflowid { get; set; }
        public string Variabel { get; set; }
        public string Operators { get; set; }
        public int? Value { get; set; }
    }
}
