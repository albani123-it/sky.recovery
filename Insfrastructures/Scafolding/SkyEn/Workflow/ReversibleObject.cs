using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class ReversibleObject
    {
        public long RroId { get; set; }
        public string RroCode { get; set; }
        public string RroName { get; set; }
        public string RroType { get; set; }
        public DateTime? RroTimestamp { get; set; }
    }
}
