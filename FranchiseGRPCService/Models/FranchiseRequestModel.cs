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

        [Column("franchise_customized_option")]
        public bool FranchiseCustomizedOption { get; set; }



        [Column("owner_id")]
        public int ownerId { get; set; }

        [Column("created_id")]
        public int CreatedId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set;}

        [Column("is_request_status")]
        public string IsRequestStatus { get; set; }

        [Column("franchise_id")]
        [ForeignKey("franchiseId")]
        public int FranchiseId { get; set; }

        public virtual FranchiseModel franchiseId { get; set; }


        public virtual ICollection<FranchiseSelectedServiceModel> FranchiseSelectedServices { get; set; }
    }
}
