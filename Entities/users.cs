using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace sky.recovery.Entities
{    
    [Table("users", Schema = "public")]

    public class users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("usr_id")]
        public int usr_id { get; set; }
        public string usr_userid { get; set; }
        public string usr_name { get; set; }
        public string usr_nip { get; set; }
        public string usr_access_level { get; set; }

     
        public string usr_branch { get; set; }
        public bool? usr_status { get; set; }
        public DateTime? usr_last_access { get; set; }
        public int? usr_is_Login { get; set; }
        public string usr_ip_address { get; set; }
        public int? usr_fail_login { get; set; }
        public string usr_email { get; set; }
        public string usr_notlp { get; set; }
        public DateTime? usr_efective_date { get; set; }
        public DateTime? usr_last_access_f { get; set; }
        public byte? usr_img_profile { get; set; }
        public string usr_supervisor { get; set; }
        public string usr_position { get; set; }
        public string usr_tnt_alias { get; set; }
        public string usr_group_alias { get; set; }
        public string usr_partner_alias { get; set; }
        public int? usr_privilege { get; set; }
        public int? usr_isfirst_login { get; set; }
        public string usr_approved_status { get; set; }
        public string usr_approved_by { get; set; }
    }
}
