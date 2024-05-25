using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class UsersPosition
    {
        public int UspId { get; set; }
        public string UspCode { get; set; }
        public string UspValue { get; set; }
        public string UspDescription { get; set; }
        public int? UspStatus { get; set; }
    }
}
