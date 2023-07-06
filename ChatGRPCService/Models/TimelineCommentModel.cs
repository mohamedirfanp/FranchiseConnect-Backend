using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biddo.Models
{
    public class TimelineCommentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("timeline_comment_id")]
        public int TimelineCommentId { get; set; }

        [Required]
        [Column("message")]
        public string message { get; set; }

        [Required]
        [Column("from_id")]
        public int FromId { get; set; }

        [Required]
        [Column("from_role")]
        public string FromRole { get; set; }

        public DateTime TimeStamp { get; set; }

        public int ConversationId { get; set; }

        public int QueryId { get; set; }





    }
}
