using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CollectionPhoto
    {
        public int Id { get; set; }
        public int? CollhistoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Mime { get; set; }
    }
}
