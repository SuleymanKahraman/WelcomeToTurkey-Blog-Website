using WelcomeToTurkeyAPI.Data.Enums;

namespace WelcomeToTurkeyAPI.Dtos.AuthDtos
{
    public class LoginResult
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }

    }
}
