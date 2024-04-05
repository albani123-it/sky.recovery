using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("rfkelurahan")]
    public class KelurahanEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("code")]
        [StringLength(50)]
        public string? Code { get; set; }

        [Column("kode_pos")]
        [StringLength(50)]
        public string? KodePos { get; set; }

        [Column("kd_dkcpl_kelurahan")]
        [StringLength(50)]
        public string? KdDkcplKelurahan { get; set; }

        [Column("name")]
        [StringLength(200)]
        public string? Name { get; set; }

        [Column("kecamatan_id")]
        public int? KecamatanId { get; set; }

        [ForeignKey(nameof(KecamatanId))]
        public KecamatanEntity? Kecamatan { get; set; }

        [Column("create_date")]
        public DateTime? CreateDate { get; set; }

        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public StatusGeneral? Status { get; set; }

    }
}