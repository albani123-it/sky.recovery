using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class ActionReq
    {
        public int Id { get; set; }
        public int? ActionId { get; set; }
        public string Code { get; set; }
        public string ActDesc { get; set; }
        public string CoreCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? StatusId { get; set; }
        public int? ReqUserId { get; set; }
        public int? ApproveUserId { get; set; }
    }
}
