using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccoutGRPCService.Models
{
    [Table("User", Schema ="dbo")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId {  get; set; }

        [Required]
        [StringLength(50)]
        [Column("user_name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(50)]
        [Column("user_phone_number")]
        public string UserPhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Column("user_role")]
        public string UserRole { get; set; }

        [Required]
        [Column("user_password")]
        public byte[] PasswordHash { get; set; }

        [Required]
        [Column("user_password_salt")]
        public byte[] PasswordSalt { get; set; }
    }
}
