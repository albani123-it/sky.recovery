using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class UserReference
    {
        public int UsrId { get; set; }
        public string UsrUserId { get; set; }
        public string UsrSupervisorId { get; set; }
        public DateTime? UsrUpdateDate { get; set; }
        public string UsrUpdateBy { get; set; }
    }
}
