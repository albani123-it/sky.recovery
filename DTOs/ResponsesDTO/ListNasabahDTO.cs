namespace sky.recovery.DTOs.ResponsesDTO
{
    public class ListNasabahDTO
    {
        public string CifNo { get; set; }
        public string AccNo { get; set; }
        public string NasabahName { get; set; }
        public string BranchName { get; set; }
        public int? Status { get; set; }
        public int? CallBy { get; set; }
    }
}
