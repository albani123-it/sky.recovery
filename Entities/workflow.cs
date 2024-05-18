using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("workflow", Schema = "RecoveryBusinessV2")]

    public class workflow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public int? fiturid { get; set; }
        public int? requestid { get; set; }
        public int? flowid { get; set; }
        public int? orders { get; set; }
public int? actor { get; set; }
        public int? status { get; set; }
        public DateTime? submitdated { get; set; }
        public DateTime? modifydated { get; set; }
        public string reason { get; set; }
        public int? masterworkflowid { get; set; }

    }
}
