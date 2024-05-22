using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities.RecoveryConfig
{
    [Table("generalparamdetail", Schema = "RecoveryBusinessV2")]

    public class GeneralParamDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public long? paramheaderid { get; set; }
        public string title { get; set; }
        public string descriptions { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
        public int? updatedby { get; set; }
        public DateTime? updateddated { get; set; }
    }
}
