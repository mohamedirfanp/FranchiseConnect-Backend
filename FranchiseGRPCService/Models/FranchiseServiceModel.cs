using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    [Table("FranchiseService", Schema = "dbo")]
    public class FranchiseServiceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_service_id")]
        public int FranchiseServiceId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("franchise_provider_service_name")]
        public string FranchiseProvideServiceName { get; set; }

        [Required]
        [Column("franchise_id")]
        [ForeignKey("franchiseId")]
        public int FranchiseId { get; set; }

        public virtual FranchiseModel franchiseId { get; set; }



    }
}
