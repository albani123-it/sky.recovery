using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Mastertemplate
    {
        public long Id { get; set; }
        public int? Fiturid { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Modifydated { get; set; }
        public int? Modifyby { get; set; }
        public int? Documenttype { get; set; }
        public int? Isactive { get; set; }
    }
}
