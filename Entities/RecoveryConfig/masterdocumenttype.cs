using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities.RecoveryConfig
{
    [Table("masterdocumenttype", Schema = "RecoveryBusinessV2")]

    public class masterdocumenttype
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public string description { get; set; }
    }
}
