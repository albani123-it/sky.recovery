using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class MasterCollateral
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string ColId { get; set; }
        public string ColType { get; set; }
        public string ColObject { get; set; }
        public string ColAddress { get; set; }
        public string ColDocument { get; set; }
        public string ColDescription { get; set; }
        public string VehBpkbNo { get; set; }
        public string VehPlateNo { get; set; }
        public string VehMerek { get; set; }
        public string VehModel { get; set; }
        public string VehBpkbName { get; set; }
        public string VehEngineNo { get; set; }
        public string VehChassisNo { get; set; }
        public string VehStnkNo { get; set; }
        public string VehYear { get; set; }
        public string VehBuildYear { get; set; }
        public string VehCc { get; set; }
        public string VehColor { get; set; }
        public DateTime? ColTglAgunan { get; set; }
        public string ColNameAgunan { get; set; }
    }
}
