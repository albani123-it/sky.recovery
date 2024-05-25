using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Reason
    {
        public long RsnId { get; set; }
        public string RsnCode { get; set; }
        public string RsnName { get; set; }
        public int? RsnIsDc { get; set; }
        public int? RsnIsFc { get; set; }
        public bool? RsnIsActive { get; set; }
        public string RsnCreatedBy { get; set; }
        public string RsnModifiedBy { get; set; }
        public bool? RsnIsDeleted { get; set; }
        public bool? RsnIsUsed { get; set; }
        public DateTime? RsnCreated { get; set; }
        public DateTime? RsnModified { get; set; }
        public string RsnApprovedStatus { get; set; }
        public string RsnApprovedBy { get; set; }
    }
}
