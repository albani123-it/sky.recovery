using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Templatedetail
    {
        public long Id { get; set; }
        public int? Mastertemplateid { get; set; }
        public int? Layoutpositionid { get; set; }
        public string Value { get; set; }
        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        public int? PositionZ { get; set; }
    }
}
