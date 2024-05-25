using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class UsersLog
    {
        public long UsrId { get; set; }
        public string UsrUserid { get; set; }
        public string UsrName { get; set; }
        public string UsrNip { get; set; }
        public string UsrAccessLevel { get; set; }
        public string UsrBranch { get; set; }
        public bool UsrStatus { get; set; }
        public DateTime? UsrLastAccess { get; set; }
        public int? UsrIsLogin { get; set; }
        public string UsrIpAddress { get; set; }
        public int? UsrFailLogin { get; set; }
        public string UsrEmail { get; set; }
        public string UsrNotlp { get; set; }
        public DateTime? UsrEfectiveDate { get; set; }
        public DateTime? UsrLastAccessF { get; set; }
        public byte[] UsrImgProfile { get; set; }
        public string UsrSupervisor { get; set; }
        public string UsrPosition { get; set; }
        public DateTime? LogActionDate { get; set; }
        public string LogUsr { get; set; }
        public string LogActionMode { get; set; }
        public long? LogId { get; set; }
        public string UsrTntAlias { get; set; }
        public string UsrGroupAlias { get; set; }
        public string UsrPartnerAlias { get; set; }
        public int? UsrPrivilege { get; set; }
        public string UsrApprovedStatus { get; set; }
        public string UsrApprovedBy { get; set; }
    }
}
