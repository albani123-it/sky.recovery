using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class ContentNotifikasi
    {
        public int CntId { get; set; }
        public string CntCode { get; set; }
        public string CntForm { get; set; }
        public string CntContent { get; set; }
        public DateTime? CntDay { get; set; }
        public bool? CntIsActive { get; set; }
        public bool? CntIsDeleted { get; set; }
        public string CntCreatedBy { get; set; }
        public string CntModifiedBy { get; set; }
        public DateTime? CntCreated { get; set; }
        public DateTime? CntModified { get; set; }
        public bool? CntRead { get; set; }
    }
}
