using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace sky.recovery.Entities
{
    public class collection_call
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public int branch_id { get; set; }
        public string acc_no { get; set; }
        public string call_name { get; set; }
        public int add_id { get; set; }
        public string call_reason { get; set; }
        public int call_result_id { get; set; }
        public DateTime call_result_date { get; set; }
        public float? call_amount { get; set; }
        public string call_notes { get; set; }
        public DateTime? call_date { get; set; }
        public int? call_by { get; set; }
        public string call_result_hh { get; set; }
        public string call_result_mm { get; set; }
        public string call_result_hhmm { get; set; }
        public int? loan_id { get; set; }
        [ForeignKey(nameof(loan_id))]
        public master_loan master_loan { get; set; }
    }
}
