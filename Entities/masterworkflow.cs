using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("masterworkflow", Schema = "RecoveryBusinessV2")]
    public class masterworkflow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
       
        public string description { get; set; }
        public int? fiturid { get; set; }
  
    }
}
