using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Rule
{
    public partial class Transactionrulecondition
    {
        public long Id { get; set; }
        public string Rulecode { get; set; }
        public string Field { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
    }
}
