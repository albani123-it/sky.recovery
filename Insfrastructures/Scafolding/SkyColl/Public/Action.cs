using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Action
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ActDesc { get; set; }
        public string CodeCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? StatusId { get; set; }
    }
}
