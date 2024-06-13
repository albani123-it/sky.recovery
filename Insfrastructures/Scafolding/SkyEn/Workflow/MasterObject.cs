using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class MasterObject
    {
        public long MobId { get; set; }
        public string MobCode { get; set; }
        public string MobName { get; set; }
        public bool? MobIsactive { get; set; }
        public string MobCreatedby { get; set; }
        public string MobUpdatedby { get; set; }
        public DateTime? MobCreatedat { get; set; }
        public DateTime? MobUpdatedat { get; set; }
    }
}
