namespace sky.recovery.DTOs.WorkflowDTO
{
    public class CallbackApprovalDTO
    {
        public int? idrequest { get; set; }
        public int? fiturid { get; set; }
        public string userid { get; set; }
        public int? status { get; set; }
        public int? workflowid { get; set; }
        public string reason { get; set; }
        public int? idrequestor { get; set; }
    }
}
