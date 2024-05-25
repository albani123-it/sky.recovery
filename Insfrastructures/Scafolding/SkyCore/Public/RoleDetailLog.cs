using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class RoleDetailLog
    {
        public long Id { get; set; }
        public int RldId { get; set; }
        public int? RldRlId { get; set; }
        public int? RldRlmId { get; set; }
        public string RldActionBy { get; set; }
        public DateTime? RldLogDate { get; set; }
        public string RldLogAction { get; set; }
        public long? RldLogid { get; set; }
    }
}
