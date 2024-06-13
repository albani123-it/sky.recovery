using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class MasterForm
    {
        public short FumId { get; set; }
        public string FumName { get; set; }
        public string FumType { get; set; }
        public string FumUrl { get; set; }
        public string FumCode { get; set; }
        public string FumTmpl { get; set; }
        public short? FumSla { get; set; }
        public string FumUsed { get; set; }
    }
}
