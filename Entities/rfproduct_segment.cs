using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    public class rfproduct_segment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("prd_sgm_id")]
        public int prd_sgm_id { get; set; }
        public string prd_sgm_code { get; set; }
        public string prd_sgm_desc { get; set; }
        public string prd_sgm_core_code { get; set; }
        public DateTime? prd_sgm_create_date { get; set; }
        public DateTime? prd_sgm_update_date { get; set; }
        public int? prd_sgm_status_id { get; set; }
    }
}
