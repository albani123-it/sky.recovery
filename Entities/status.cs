using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("sts_id")]
        public int sts_id { get; set; }
        public string sts_name { get; set; }
        public string sts_type { get; set; }
    }
}
