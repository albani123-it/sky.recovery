using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Generalparamdetail
    {
        public long Id { get; set; }
        public int? Paramheaderid { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Updatedby { get; set; }
        public DateTime? Updateddated { get; set; }
    }
}
