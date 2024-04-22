using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("lbrc_id")]
        public int lbrc_id { get; set; }
        public string lbrc_code { get; set; }
        public string lbrc_name { get; set; }
        public string lbrc_address { get; set; }
        public string lbrc_city { get; set; }
        public string lbrc_phone_num { get; set; }
        public bool? lbrc_is_delete { get; set; }
        public string lbrc_group { get; set; }
    }
}
