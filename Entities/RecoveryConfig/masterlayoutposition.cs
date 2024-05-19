using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities.RecoveryConfig
{
    [Table("masterlayoutposition", Schema = "RecoveryBusinessV2")]

    public class masterlayoutposition
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public string description { get; set; }

    }
}
