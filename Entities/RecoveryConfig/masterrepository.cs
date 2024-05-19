using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities.RecoveryConfig
{
    [Table("masterrepository", Schema = "RecoveryBusinessV2")]


    public class masterrepository
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int? userid { get; set; }
        public int? fiturid { get; set; }
        public string filename { get; set; }
        public string fileurl { get; set; }
        public DateTime? uploaddated { get; set; }
        public DateTime? modifydated { get; set; }
        public int? isactive { get; set; }
    }
}
