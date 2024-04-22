using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace sky.recovery.Entities
{
    public class collection_add_contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public string add_id { get; set; }
        public string cu_cif { get; set; }
        public string acc_no { get; set; }
        public string add_phone { get; set; }
        public string add_address { get; set; }

        public string add_city { get; set; }
        public string add_from { get; set; }
        public int add_by { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public DateTime add_date { get; set; }
    }
}
