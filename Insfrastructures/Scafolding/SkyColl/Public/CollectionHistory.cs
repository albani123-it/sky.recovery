using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CollectionHistory
    {
        public int Id { get; set; }
        public int? CallId { get; set; }
        public int? BranchId { get; set; }
        public string AccNo { get; set; }
        public string Name { get; set; }
        public int? AddId { get; set; }
        public int? Reason { get; set; }
        public int? Result { get; set; }
        public DateTime? ResultDate { get; set; }
        public double? Amount { get; set; }
        public string Note { get; set; }
        public DateTime? HistoryDate { get; set; }
        public int? HistoryBy { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Picture { get; set; }
        public string UbmId { get; set; }
        public string CbmId { get; set; }
        public string Kolek { get; set; }
        public string Callresulthh { get; set; }
        public string CallResultHhmm { get; set; }
        public string CallResultMm { get; set; }
        public int? Dpd { get; set; }
        public int? CallBy { get; set; }
    }
}
