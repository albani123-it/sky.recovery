using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class rfproduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("prd_id")]
        public int prd_id { get; set; }
        public string prd_code { get; set; }
        public string prd_desc { get; set; }
        public string prd_core_code { get; set; }
        public int prd_sgm_id { get; set; }
        public int? prd_status_id { get; set; }
    }
}
