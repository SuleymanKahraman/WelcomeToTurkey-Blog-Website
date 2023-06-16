using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using WelcomeToTurkeyAPI.Data.Enums;

namespace WelcomeToTurkeyAPI.Dtos.UsersDtos
{
    public class ListOfUsersByFilter
    {
        
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public UserTypes UserType { get; set; }
    }
}
