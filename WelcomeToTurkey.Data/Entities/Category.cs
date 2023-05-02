using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeToTurkeyAPI.Data.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
           
    }
}
