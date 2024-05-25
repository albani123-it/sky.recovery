using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CollectionCall
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string AccNo { get; set; }
        public string CallName { get; set; }
        public int? AddId { get; set; }
        public int? CallReason { get; set; }
        public int? CallResultId { get; set; }
        public DateTime? CallResultDate { get; set; }
        public double? CallAmount { get; set; }
        public string CallNotes { get; set; }
        public DateTime? CallDate { get; set; }
        public int? CallBy { get; set; }
        public string CallResultHh { get; set; }
        public string CallResultMm { get; set; }
        public string CallResultHhmm { get; set; }
        public int? LoanId { get; set; }
    }
}
