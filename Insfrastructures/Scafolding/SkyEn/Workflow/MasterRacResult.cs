using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class MasterRacResult
    {
        public long MrsId { get; set; }
        public string MrsFieldName { get; set; }
        public string MrsKriteria { get; set; }
        public string MrsValueRecommendation { get; set; }
        public string MrsRejectCode { get; set; }
    }
}
