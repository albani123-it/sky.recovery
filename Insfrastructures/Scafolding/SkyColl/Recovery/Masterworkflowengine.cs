using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Masterworkflowengine
    {
        public long Id { get; set; }
        public string Wfcode { get; set; }
        public string Wfname { get; set; }
        public long? Fiturid { get; set; }
        public bool? Isactive { get; set; }
        public long? Flowid { get; set; }
    }
}
