﻿using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Restrukturedokuman
    {
        public long Id { get; set; }
        public int? Typedocid { get; set; }
        public string Doctypedesc { get; set; }
        public string Filepath { get; set; }
        public string Fileurl { get; set; }
        public DateTime? Uploaddated { get; set; }
        public int? Uploadedby { get; set; }
        public DateTime? Modifydated { get; set; }
        public int? Modifiedby { get; set; }
        public int? Loanid { get; set; }
        public int? Restruktureid { get; set; }
    }
}
