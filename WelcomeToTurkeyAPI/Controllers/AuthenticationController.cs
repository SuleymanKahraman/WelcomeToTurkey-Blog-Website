using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data;
using WelcomeToTurkeyAPI.Data.Entities;
using WelcomeToTurkeyAPI.Dtos;

namespace WelcomeToTurkeyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly WTTDbContext dbContext;

        public AuthenticationController(WTTDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("sign_up")]

        public IActionResult SignUp([FromBody] SignUpDto opt)
        {

            var signUpEntity = new User()
            {
                FirstName = opt.FirstName,
                LastName = opt.LastName,
                EmailAdress = opt.Email,
                Password = opt.Password
            };

            if (signUpEntity != null)
            {
                dbContext.Users.Add(signUpEntity);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
