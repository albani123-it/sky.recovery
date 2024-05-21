using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("master_collateral", Schema = "public")]

    public class master_collateral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int? loan_id { get; set; }
        public string col_id { get; set; }
        public string col_type{get;set;}
        public string col_address { get; set; }
        public string col_document { get; set; }
        public string col_description { get; set; }
        public string veh_bpkb_no { get; set; }
        public string veh_plate_no { get; set; }
        public string veh_merek { get; set; }
        public string veh_model { get; set; }
        public string veh_bpkb_name { get; set; }

        public string veh_engine_no { get; set; }
        public string veh_chassis_no { get; set; }
        public string veh_stnk_no { get; set; }
        public string veh_year { get; set; }
        public string veh_build_year { get; set; }
        public string veh_cc { get; set; }
        public string veh_color { get; set; }
        public DateTime? col_tgl_agunan { get; set; }
        public string col_name_agunan { get; set; }
    }
}
