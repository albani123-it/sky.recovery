using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Rule
{
    public partial class Transactionruleaction
    {
        public long Id { get; set; }
        public string Rulecode { get; set; }
        public string Actiontype { get; set; }
        public string Actionoptions { get; set; }
    }
}
