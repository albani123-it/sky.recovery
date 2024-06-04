using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Rule
{
    public partial class Repository
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Urlname { get; set; }
        public DateTime? Uploadeddated { get; set; }
        public int? Uploadedby { get; set; }
        public string Status { get; set; }
        public string Exceptions { get; set; }
        public int? Datasourceid { get; set; }
        public int? Ruleid { get; set; }
    }
}
