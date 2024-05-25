using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class ActionGroup
    {
        public int Id { get; set; }
        public int? ActionId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? StatusId { get; set; }
    }
}
