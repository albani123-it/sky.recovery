using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class IrreversibleObject
    {
        public long IroId { get; set; }
        public string IroCode { get; set; }
        public string IroName { get; set; }
        public string IroType { get; set; }
        public DateTime? IroTimestamp { get; set; }
    }
}
