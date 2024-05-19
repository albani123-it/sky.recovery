using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities.RecoveryConfig
{
    [Table("mastertemplate", Schema = "RecoveryBusinessV2")]

    public class mastertemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int id { get; set; }
        public int? fiturid { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
        public string title { get; set; }

        public string description { get; set; }
        public DateTime? modifydated { get; set; }
        public int? modifyby { get; set; }
        public int? documenttype { get; set; }
        public int? isactive { get; set; }

    }
}
