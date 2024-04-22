namespace sky.recovery.DTOs.ResponsesDTO
{
    public class MonitoringDetailRestructureDTO
    {
        public string acc_no { get; set; }
        public string cucif { get; set; }
        public string Nasabah { get; set; }
        public double? TotalKewajiban{ get; set; }
        public int? DPD { get; set; }
        public int? Kolektibilitas{ get; set; }
        public int? CallBy { get; set; }
        public int? Status { get; set; }
    }
}
