namespace sky.recovery.DTOs.ResponsesDTO.Restrukture
{
    public class GetPolaRestrukDTO
    {
        public int idrestrukture { get; set; }
        public int? idloan { get; set; }
        public int? branchid { get; set; }
        public string cabang { get; set; }
    public long? polaid { get; set; }
        public string pola { get; set; }
        public string keterangan { get; set; }
        public decimal? pengurangannilaimargin { get; set; }
        public long? jenispenguranganid { get; set; }
        public string jenispengurangan { get; set; }
    public int? graceperiode { get; set; }
    
    
    
    }



}
