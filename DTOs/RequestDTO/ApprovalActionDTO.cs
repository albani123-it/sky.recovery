namespace sky.recovery.DTOs.RequestDTO
{
    public class ApprovalActionDTO
    {
        public string UserId { get; set; }
        public int? idrestrukture { get; set; }
        public string actionreason { get; set; }
        public int? workflowid { get; set; }
        public string actions { get; set; }
    }
}
