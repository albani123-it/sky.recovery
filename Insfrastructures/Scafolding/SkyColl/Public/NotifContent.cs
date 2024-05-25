using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class NotifContent
    {
        public long LscId { get; set; }
        public string LscCode { get; set; }
        public string LscName { get; set; }
        public string LscContent { get; set; }
        public string LscDay { get; set; }
        public bool? LscIsActive { get; set; }
        public string LscCreatedBy { get; set; }
        public string LscModifiedBy { get; set; }
        public bool? LscIsDeleted { get; set; }
        public bool? LscIsUsed { get; set; }
        public DateTime? LscCreated { get; set; }
        public DateTime? LscModified { get; set; }
        public string LscApprovedStatus { get; set; }
        public string LscApprovedBy { get; set; }
    }
}
