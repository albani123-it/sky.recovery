using System;

namespace sky.recovery.DTOs.RequestDTO
{
    public class AddPolaDTO
    {
        public string userid { get; set; }
        public int? idloan { get; set; }
        public int? idrestrukture { get; set; }
        public int? branchid { get; set; }
        public string keterangan { get; set; }
        public int? PolaId { get; set; }
        public int? pengurangannilaimargin { get; set; }
        public int? jenispengurangan { get; set; }
        public int? graceperiode { get; set; }
        //public DateTime? tgljatuhtempobaru { get; set; }
    }
}
