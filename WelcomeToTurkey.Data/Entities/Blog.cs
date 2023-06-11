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
        public string Title { get; set; }    

        
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public int CategoryId { get; set; }

       
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [Required]
        public bool IsPublished { get; set; }
    }


}
