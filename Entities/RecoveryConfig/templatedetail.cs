using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities.RecoveryConfig
{
    public class templatedetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int? mastertemplateid { get; set; }
        public int? layoutpositionid { get; set; }
        public string value { get; set; }
        public int? position_x { get; set; }
        public int? position_y { get; set; }
        public int? position_z { get; set; }


    }
}
