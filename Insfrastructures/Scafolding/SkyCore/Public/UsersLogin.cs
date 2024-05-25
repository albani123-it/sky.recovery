using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class UsersLogin
    {
        public long UslId { get; set; }
        public string UslUsername { get; set; }
        public string UslPassword { get; set; }
        public string UslCreatedby { get; set; }
        public DateTime? UslCreateddate { get; set; }
        public string UslChangedby { get; set; }
        public DateTime? UslChangeddate { get; set; }
        public string UslPassHistory1 { get; set; }
        public string UslPassHistory2 { get; set; }
        public string UslPassHistory3 { get; set; }
        public string UslPassHistory4 { get; set; }
        public string UslPassHistory5 { get; set; }
    }
}
