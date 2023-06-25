using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    public class FranchiseRequestModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_request_id")]
        public int FranchiseRequestId { get; set; }

        [Column("franchise_sample_box_option")]
        public bool FranchiseSampleBoxOption { get; set; }

        [Column("franchise_request_report")]
        [StringLength(250)]
        public string FranchiseRequestReport { get; set; }

        [Column("franchise_id")]
        [ForeignKey("franchiseId")]
        public int FranchiseId { get; set; }

        public virtual FranchiseModel franchiseId { get; set; }
    }
}
