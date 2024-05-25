using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class MenuListLog
    {
        public int? Id { get; set; }
        public string MnName { get; set; }
        public string MnLink { get; set; }
        public int? MnParentid { get; set; }
        public string MnAcl { get; set; }
        public int? MnOrder { get; set; }
        public string MnIcon { get; set; }
        public string Breadcrumb { get; set; }
        public string LogAction { get; set; }
        public DateTime? LogDate { get; set; }
        public long? LogId { get; set; }
    }
}
