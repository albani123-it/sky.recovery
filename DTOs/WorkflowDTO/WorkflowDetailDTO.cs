namespace sky.recovery.DTOs.WorkflowDTO
{
    public class WorkflowDetailDTO
    {
        public long Id { get; set; }
        public int? fiturid { get; set; }
        public string Status { get; set; }
        
        public string fitur { get; set; }
        public long? requestid { get; set; }
        public int? flowid { get; set; }
        public int? actor { get; set; }
        public int? statusid { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDated { get; set; }
    }
}
