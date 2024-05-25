using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CollectionAddContact
    {
        public int Id { get; set; }
        public string AddId { get; set; }
        public string CuCif { get; set; }
        public string AccNo { get; set; }
        public string AddPhone { get; set; }
        public string AddAddress { get; set; }
        public string AddCity { get; set; }
        public string AddFrom { get; set; }
        public DateTime? AddDate { get; set; }
        public int? AddBy { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? Def { get; set; }
    }
}
