using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CollectionVisit
    {
        public int Id { get; set; }
        public string VisitId { get; set; }
        public int? BranchId { get; set; }
        public string AccNo { get; set; }
        public string VisitName { get; set; }
        public string AddId { get; set; }
        public string VisitReason { get; set; }
        public int? VisitResult { get; set; }
        public DateTime? VisitResultDate { get; set; }
        public double? VisitAmount { get; set; }
        public string VisitNote { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitBy { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Picture { get; set; }
        public string UbmId { get; set; }
        public string CbmId { get; set; }
        public string Kolek { get; set; }
    }
}
