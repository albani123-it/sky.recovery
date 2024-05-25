using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CallScript
    {
        public long CscId { get; set; }
        public string CscCode { get; set; }
        public string CscDesc { get; set; }
        public int? CscAccdMin { get; set; }
        public int? CscAccdMax { get; set; }
        public string CscCsScript { get; set; }
        public bool? CscIsActive { get; set; }
        public string CscCreatedBy { get; set; }
        public string CscModifiedBy { get; set; }
        public bool? CscIsDeleted { get; set; }
        public bool? CscIsUsed { get; set; }
        public DateTime? CscCreated { get; set; }
        public DateTime? CscModified { get; set; }
        public string CscApprovedStatus { get; set; }
        public string CscApprovedBy { get; set; }
    }
}
