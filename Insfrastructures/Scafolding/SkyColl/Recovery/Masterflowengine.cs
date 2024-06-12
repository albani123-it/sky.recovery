using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Masterflowengine
    {
        public long Id { get; set; }
        public long? Flowid { get; set; }
        public string Nodesid { get; set; }
        public string Title { get; set; }
        public int? Orders { get; set; }
        public long? Fiturid { get; set; }
    }
}
