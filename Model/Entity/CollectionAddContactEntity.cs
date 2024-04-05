using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("collection_add_contact")]
    public class CollectionAddContactEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("add_id")]
        [StringLength(50)]
        public string? AddId { get; set; }

        [Column("cu_cif")]
        [StringLength(20)]
        public string? CuCif { get; set; }

        [Column("acc_no")]
        [StringLength(20)]
        public string? AccNo { get; set; }

        [Column("add_phone")]
        [StringLength(30)]
        public string? AddPhone { get; set; }

        [Column("add_address")]
        [StringLength(200)]
        public string? AddAddress { get; set; }

        [Column("add_city")]
        [StringLength(50)]
        public string? AddCity { get; set; }

        [Column("add_from")]
        [StringLength(25)]
        public string? AddFrom { get; set; }

        [Column("lat")]
        public double? Lat { get; set; }

        [Column("lon")]
        public double? Lon { get; set; }

        [Column("add_date", TypeName = "datetime")]
        public DateTime? AddDate { get; set; }

        [Column("add_by")]
        public int? AddById { get; set; }

        [Column("def")]
        public int? Default { get; set; }

        [ForeignKey(nameof(AddById))]
        public UserEntity? AddBy { get; set; }

        public virtual ICollection<CollectionContactPhotoEntity>? Photo { get; set; }
    }
}