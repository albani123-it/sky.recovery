using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("rfkecamatan")]
    public class KecamatanEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("code")]
        [StringLength(50)]
        public string? Code { get; set; }

        [Column("name")]
        [StringLength(200)]
        public string? Name { get; set; }

        [Column("kabupaten_id")]
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }

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