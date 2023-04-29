using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeToTurkeyAPI.Data.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BlogId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(BlogId))]
        public virtual Blog Blog { get; set; }
    }


}
