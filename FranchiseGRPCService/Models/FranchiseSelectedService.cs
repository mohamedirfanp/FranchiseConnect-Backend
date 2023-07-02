using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    public class FranchiseSelectedService
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_selected_service_id")]
        public int FranchiseSelectedServiceId { get; set; }



        [Column("franchise_provide_service_id")]
        [ForeignKey("FranchiseServiceId")]
        public int FranchiseProvideServiceId { get; set; }


        [Column("franchise_customized_option_id")]
        [ForeignKey("FranchiseCustomizedOption")]

        public int FranchiseCustomizedOptionId { get; set; }

        [Column("user_customization")]
        public bool isUserCustomization { get; set; } = false;


        public virtual FranchiseServiceModel FranchiseServiceId { get; set; }

        public virtual FranchiseCustomizedOptionModel FranchiseCustomizedOption { get; set; }


    }
}
