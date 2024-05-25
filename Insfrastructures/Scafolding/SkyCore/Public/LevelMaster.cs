using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class LevelMaster
    {
        public int Id { get; set; }
        public string LmName { get; set; }
        public string LmDescription { get; set; }
        public int? LmParentid { get; set; }
        public int? LmUrut { get; set; }
        public string LmModul { get; set; }
    }
}
