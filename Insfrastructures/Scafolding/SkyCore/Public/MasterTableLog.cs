using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class MasterTableLog
    {
        public int TblId { get; set; }
        public string TblCode { get; set; }
        public string TblName { get; set; }
        public string TblDatabase { get; set; }
        public string TblSchema { get; set; }
        public string TblUniqueField { get; set; }
        public string TblFor { get; set; }
        public string TblTmpField { get; set; }
        public string TblUniqueFieldType { get; set; }
        public string TblUsrby { get; set; }
        public DateTime? TblTimestamp { get; set; }
        public long? TblLogid { get; set; }
        public string TblAction { get; set; }
    }
}
