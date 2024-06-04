using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Transactionruleactionbucket
    {
        public long Id { get; set; }
        public string Rulecode { get; set; }
        public string Bucketcode { get; set; }
        public int? Bucketid { get; set; }
    }
}
