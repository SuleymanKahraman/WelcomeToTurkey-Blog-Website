using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto opt)
        {
            var loginResult = dbContext.Users.Where(x => x.EmailAdress == opt.Email && x.Password == opt.Password)
                                             .Select(x=> new LoginResult { FirstName = x.FirstName, LastName = x.LastName, UserId = x.Id}).SingleOrDefault();
            if (loginResult != null)
            {
                var token = GetJwtToken(loginResult.UserId);
                loginResult.Token = token;
                return Ok(loginResult);

            }
            return BadRequest();
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

        private string GetJwtToken(int userId)
        {
            var claims = new Dictionary<string, object>
            {
                { "UserId", userId },
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Hava ne kadar guzel!")),
                                                            SecurityAlgorithms.HmacSha256Signature),
                Claims = claims,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
