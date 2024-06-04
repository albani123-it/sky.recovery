using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Sourcedatum
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Rulename { get; set; }
        public int? Datasourceid { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime? Endtime { get; set; }
        public string Status { get; set; }
        public int? Repositoryid { get; set; }
    }
}
