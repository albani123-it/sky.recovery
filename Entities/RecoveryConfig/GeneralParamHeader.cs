using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities.RecoveryConfig
{
    [Table("generalparamheader", Schema = "RecoveryBusinessV2")]

    public class GeneralParamHeader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long id { get; set; }
        public string title { get; set; }
        public string descriptions { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
        public int? updatedby { get; set; }
        public DateTime? updateddated { get; set; }
    }
}
