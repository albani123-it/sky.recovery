using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    public class restructure_cashflow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rsc_id")]
        public int rsc_id { get; set; }
        public int? rsc_penghasilan_nasabah{ get; set; }
        //public int? rsc_penghasilan_bersih { get; set; }
        public int? rsc_penghasilan_pasangan { get; set; }
        public int? rsc_penghasilan_lainnya { get; set; }
        public int? rsc_total_penghasilan { get; set; }
        public int? rsc_biaya_pendidikan { get; set; }
        public int? rsc_biaya_listrik_air_telp { get; set; }
        public int? rsc_biaya_belanja { get; set;}

        public int? rsc_biaya_transportasi { get; set; }
        public int? rsc_biaya_lainnya { get; set; }
        public int? rsc_total_pengeluaran { get; set; }
        public int? rsc_hutang_di_bank { get; set; }
        public int? rsc_cicilan_lainnya { get; set; }
        public int? rsc_total_kewajiban { get; set; }
        public int? rsc_pengasilan_bersih { get; set; }
        public int? rsc_rpc_70_persen { get; set; }
        [ForeignKey(nameof(rsc_restructure_id))]
        public restructure? restructure { get; set; }
        public int? rsc_restructure_id { get; set; }

    }
}
