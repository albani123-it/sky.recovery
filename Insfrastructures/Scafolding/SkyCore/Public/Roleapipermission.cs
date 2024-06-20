using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class Roleapipermission
    {
        public long RpId { get; set; }
        public int? RlId { get; set; }
        public bool? Isaccessed { get; set; }
        public string Urlapi { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
