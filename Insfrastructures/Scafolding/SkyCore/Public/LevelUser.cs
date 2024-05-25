using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class LevelUser
    {
        public int LuId { get; set; }
        public string LuLevelCode { get; set; }
        public string LuLevelName { get; set; }
        public string LuLevelDescription { get; set; }
        public bool LuStatus { get; set; }
        public decimal? LuLimitFrom { get; set; }
        public decimal? LuLimitTo { get; set; }
    }
}
