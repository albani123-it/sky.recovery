using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class Privilege
    {
        public int PriId { get; set; }
        public string PriFunction { get; set; }
        public decimal? PriPlafonForm { get; set; }
        public decimal? PriPlafonTo { get; set; }
        public string PriRole { get; set; }
        public string PriStatus { get; set; }
    }
}
