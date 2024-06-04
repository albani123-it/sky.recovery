using System;

namespace sky.recovery.DTOs.WorkflowDTO
{
    public class WorkflowHistoryDTO
    {
        public long id { get; set; }
        public int? workflowid { get; set; }
        public int? actor { get; set; }
        public int? statusid { get; set; }
        public string status { get; set; }
        public DateTime? dated { get; set; }
        public string reason { get; set; }
        public string ActoredBy { get; set; }
    }
}
