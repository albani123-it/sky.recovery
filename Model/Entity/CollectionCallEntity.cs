using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("collection_call")]
    public class CollectionCallEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("loan_id")]
        public int? LoanId { get; set; }

        [ForeignKey(nameof(LoanId))]
        public MasterLoanEntity? Loan { get; set; }

        [Column("branch_id")]
        public int? BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }

        [Column("acc_no")]
        [StringLength(25)]
        public string? AccNo { get; set; }

        [Column("call_name")]
        [StringLength(50)]
        public string? CallName { get; set; }

        [Column("add_id")]
        public int? CollectionAddId { get; set; }

        [ForeignKey(nameof(CollectionAddId))]
        public CollectionAddContactEntity? CollectionAdd { get; set; }

        [Column("call_reason")]
        public int? ReasonId { get; set; }

        [ForeignKey(nameof(ReasonId))]
        public ReasonEntity? Reason { get; set; }

        [Column("call_result_id")]
        public int? CallResultId { get; set; }

        [ForeignKey(nameof(CallResultId))]
        public CallResultEntity? CallResult { get; set; }

        [Column("call_result_date", TypeName = "datetime")]
        public DateTime? CallResultDate { get; set; }


        [Column("call_amount")]
        public double? CallAmount { get; set; }

        [Column("call_notes")]
        [StringLength(1000)]
        public string? CallNotes { get; set; }

        [Column("call_date", TypeName = "datetime")]
        public DateTime? CallDate { get; set; }

        [Column("call_by")]
        public int? CallById { get; set; }

        [ForeignKey(nameof(CallById))]
        public UserEntity? CallBy { get; set; }

        [Column("call_result_hh")]
        [StringLength(2)]
        public string? CallResultHh { get; set; }

        [Column("call_result_mm")]
        [StringLength(2)]
        public string? CallResultMm { get; set; }

        [Column("call_result_hhmm")]
        [StringLength(5)]
        public string? CallResultHhmm { get; set; }
    }
}