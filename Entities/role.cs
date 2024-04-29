using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rl_id")]
        public int rl_id { get; set; }
        public string rl_name { get; set; }
        public string rl_description { get; set; }
        public bool? rl_status { get; set; }
        public string rl_created_by { get; set; }
        public DateTime? rl_created_date { get; set; }
        public string rl_updated_by { get; set; }
        public DateTime? rl_updated_date { get; set; }
    }
}
