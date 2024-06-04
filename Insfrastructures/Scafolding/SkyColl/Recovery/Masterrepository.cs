using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Masterrepository
    {
        public long Id { get; set; }
        public int? Userid { get; set; }
        public string Filename { get; set; }
        public string Fileurl { get; set; }
        public DateTime? Uploaddated { get; set; }
        public DateTime? Modifydated { get; set; }
        public int? Isactive { get; set; }
        public int? Fiturid { get; set; }
        public int? Requestid { get; set; }
        public int? Doctype { get; set; }
    }
}
