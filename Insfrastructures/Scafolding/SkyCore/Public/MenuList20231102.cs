using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class MenuList20231102
    {
        public int? Id { get; set; }
        public string MnName { get; set; }
        public string MnLink { get; set; }
        public int? MnParentid { get; set; }
        public string MnAcl { get; set; }
        public int? MnOrder { get; set; }
        public string MnIcon { get; set; }
        public string Breadcrumb { get; set; }
        public string MnModule { get; set; }
    }
}
