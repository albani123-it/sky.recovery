using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("workflowhistory", Schema = "RecoveryBusinessV2")]

    public class workflowhistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public int? workflowid { get; set; }
        public int? actor { get; set; }
        public int? status { get; set; }
        public DateTime? dated { get; set; }
        public string reason { get; set; }

    }
}
