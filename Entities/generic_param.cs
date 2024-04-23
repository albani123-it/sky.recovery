using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class generic_param
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("glp_id")]
        public int? glp_id { get; set; }

        [Column("glp_name")]
        public string? glp_name { get; set; }

        [Column("glp_code")]
        public string? glp_code { get; set; }
    }
}

