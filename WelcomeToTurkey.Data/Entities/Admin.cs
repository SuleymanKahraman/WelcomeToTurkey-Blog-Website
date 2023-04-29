using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeToTurkeyAPI.Data.Entities
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string EmailAdress { get; set; }
        [Required] 
        public string Password { get; set; }


    }


}
