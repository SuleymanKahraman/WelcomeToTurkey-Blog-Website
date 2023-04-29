using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeToTurkeyAPI.Data.Entities
{
    [Table("Blogs")]
    public class Blog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
    }


}
