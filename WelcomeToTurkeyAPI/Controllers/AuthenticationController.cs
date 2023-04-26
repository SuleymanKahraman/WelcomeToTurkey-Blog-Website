using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data;

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

    }
}
